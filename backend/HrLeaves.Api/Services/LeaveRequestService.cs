using System.Text;
using HrLeaves.Api.Data;
using HrLeaves.Api.DTOs;
using HrLeaves.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HrLeaves.Api.Services;

public class LeaveRequestService(AppDbContext db) : ILeaveRequestService
{
    public async Task<List<LeaveRequestDto>> GetAsync(string? status, int? leaveTypeId, DateOnly? fromDate, DateOnly? toDate)
    {
        var query = db.LeaveRequests.Include(x => x.Employee).Include(x => x.LeaveType).AsQueryable();
        if (Enum.TryParse<LeaveRequestStatus>(status, true, out var parsed)) query = query.Where(x => x.Status == parsed);
        if (leaveTypeId.HasValue) query = query.Where(x => x.LeaveTypeId == leaveTypeId);
        if (fromDate.HasValue) query = query.Where(x => x.StartDate >= fromDate);
        if (toDate.HasValue) query = query.Where(x => x.EndDate <= toDate);
        return await query.OrderByDescending(x => x.CreatedAt).Select(x => x.ToDto()).ToListAsync();
    }

    public async Task<LeaveRequestDto> CreateAsync(CreateLeaveRequestDto dto)
    {
        if (dto.StartDate > dto.EndDate) throw new InvalidOperationException("StartDate must be less than or equal to EndDate.");
        var days = CountBusinessDays(dto.StartDate, dto.EndDate);
        if (days <= 0) throw new InvalidOperationException("Selected date range does not contain working days.");

        var balance = await db.LeaveBalances.Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.EmployeeId == dto.EmployeeId && x.LeaveTypeId == dto.LeaveTypeId)
            ?? throw new InvalidOperationException("Leave balance not found for employee and leave type.");

        if (balance.RemainingDays < days) throw new InvalidOperationException("Insufficient leave balance.");

        var overlaps = await db.LeaveRequests.AnyAsync(x => x.EmployeeId == dto.EmployeeId &&
            (x.Status == LeaveRequestStatus.Pending || x.Status == LeaveRequestStatus.Approved) &&
            dto.StartDate <= x.EndDate && dto.EndDate >= x.StartDate);
        if (overlaps) throw new InvalidOperationException("Leave request overlaps with an existing pending or approved request.");

        var request = new LeaveRequest
        {
            EmployeeId = dto.EmployeeId,
            LeaveTypeId = dto.LeaveTypeId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            DaysRequested = days,
            Reason = dto.Reason.Trim(),
            Status = LeaveRequestStatus.Pending
        };
        db.LeaveRequests.Add(request);
        await db.SaveChangesAsync();
        return (await db.LeaveRequests.Include(x => x.Employee).Include(x => x.LeaveType).FirstAsync(x => x.Id == request.Id)).ToDto();
    }

    public async Task<LeaveRequestDto> ApproveAsync(int id)
    {
        var request = await LoadRequest(id);
        if (request.Status != LeaveRequestStatus.Pending) throw new InvalidOperationException("Only pending requests can be approved.");
        var balance = await db.LeaveBalances.FirstAsync(x => x.EmployeeId == request.EmployeeId && x.LeaveTypeId == request.LeaveTypeId);
        if (balance.RemainingDays < request.DaysRequested) throw new InvalidOperationException("Insufficient leave balance.");
        balance.UsedDays += request.DaysRequested;
        request.Status = LeaveRequestStatus.Approved;
        await db.SaveChangesAsync();
        return request.ToDto();
    }

    public async Task<LeaveRequestDto> RejectAsync(int id, string? comment)
    {
        var request = await LoadRequest(id);
        if (request.Status != LeaveRequestStatus.Pending) throw new InvalidOperationException("Only pending requests can be rejected.");
        request.Status = LeaveRequestStatus.Rejected;
        request.RejectionComment = comment;
        await db.SaveChangesAsync();
        return request.ToDto();
    }

    public async Task BulkApproveAsync(List<int> ids) { foreach (var id in ids.Distinct()) await ApproveAsync(id); }
    public async Task BulkRejectAsync(List<int> ids, string? comment) { foreach (var id in ids.Distinct()) await RejectAsync(id, comment); }

    public async Task<string> ExportCsvAsync()
    {
        var rows = await db.LeaveRequests.Include(x => x.Employee).Include(x => x.LeaveType).OrderByDescending(x => x.CreatedAt).ToListAsync();
        var sb = new StringBuilder("Id,Employee,LeaveType,StartDate,EndDate,Days,Status,Reason\n");
        foreach (var r in rows) sb.AppendLine($"{r.Id},\"{r.Employee.FullName}\",\"{r.LeaveType.Name}\",{r.StartDate},{r.EndDate},{r.DaysRequested},{r.Status},\"{r.Reason.Replace("\"", "\"\"")}\"");
        return sb.ToString();
    }

    private async Task<LeaveRequest> LoadRequest(int id) => await db.LeaveRequests.Include(x => x.Employee).Include(x => x.LeaveType).FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException("Leave request not found.");

    private static int CountBusinessDays(DateOnly start, DateOnly end)
    {
        var count = 0;
        for (var date = start; date <= end; date = date.AddDays(1))
            if (date.DayOfWeek is not DayOfWeek.Saturday and not DayOfWeek.Sunday) count++;
        return count;
    }
}
