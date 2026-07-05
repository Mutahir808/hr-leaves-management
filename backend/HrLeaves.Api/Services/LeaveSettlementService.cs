using HrLeaves.Api.Data;
using HrLeaves.Api.DTOs;
using HrLeaves.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HrLeaves.Api.Services;

public class LeaveSettlementService(AppDbContext db) : ILeaveSettlementService
{
    public async Task<List<LeaveSettlementDto>> GetAsync() => await db.LeaveSettlements.Include(x => x.Employee).Include(x => x.LeaveType).OrderByDescending(x => x.CreatedAt).Select(x => x.ToDto()).ToListAsync();

    public async Task<LeaveSettlementDto> CreateAsync(CreateSettlementDto dto)
    {
        var balance = await db.LeaveBalances.FirstOrDefaultAsync(x => x.EmployeeId == dto.EmployeeId && x.LeaveTypeId == dto.LeaveTypeId)
            ?? throw new InvalidOperationException("Leave balance not found.");
        balance.TotalDays += dto.AdjustmentDays;
        if (balance.TotalDays < balance.UsedDays) throw new InvalidOperationException("Adjustment cannot make total balance less than used days.");
        var settlement = new LeaveSettlement { EmployeeId = dto.EmployeeId, LeaveTypeId = dto.LeaveTypeId, AdjustmentDays = dto.AdjustmentDays, Reason = dto.Reason.Trim() };
        db.LeaveSettlements.Add(settlement);
        await db.SaveChangesAsync();
        return (await db.LeaveSettlements.Include(x => x.Employee).Include(x => x.LeaveType).FirstAsync(x => x.Id == settlement.Id)).ToDto();
    }
}
