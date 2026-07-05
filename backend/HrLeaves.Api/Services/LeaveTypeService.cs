using HrLeaves.Api.Data;
using HrLeaves.Api.DTOs;
using HrLeaves.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HrLeaves.Api.Services;

public class LeaveTypeService(AppDbContext db) : ILeaveTypeService
{
    public async Task<List<LeaveTypeDto>> GetAsync() => await db.LeaveTypes.OrderBy(x => x.Name).Select(x => x.ToDto()).ToListAsync();

    public async Task<LeaveTypeDto> CreateAsync(UpsertLeaveTypeDto dto)
    {
        if (await db.LeaveTypes.AnyAsync(x => x.Name == dto.Name)) throw new InvalidOperationException("Leave type already exists.");
        var type = new LeaveType { Name = dto.Name.Trim(), DefaultDays = dto.DefaultDays, IsAccrued = dto.IsAccrued, AccrualRatePerMonth = dto.AccrualRatePerMonth };
        db.LeaveTypes.Add(type);
        await db.SaveChangesAsync();

        var employees = await db.Employees.ToListAsync();
        foreach (var employee in employees)
        {
            db.LeaveBalances.Add(new LeaveBalance
            {
                EmployeeId = employee.Id,
                LeaveTypeId = type.Id,
                TotalDays = type.DefaultDays,
                UsedDays = 0
            });
        }
        await db.SaveChangesAsync();
        return type.ToDto();
    }

    public async Task<LeaveTypeDto> UpdateAsync(int id, UpsertLeaveTypeDto dto)
    {
        var type = await db.LeaveTypes.FindAsync(id) ?? throw new KeyNotFoundException("Leave type not found.");
        type.Name = dto.Name.Trim(); type.DefaultDays = dto.DefaultDays; type.IsAccrued = dto.IsAccrued; type.AccrualRatePerMonth = dto.AccrualRatePerMonth;
        await db.SaveChangesAsync();
        return type.ToDto();
    }

    public async Task DeleteAsync(int id)
    {
        var type = await db.LeaveTypes.FindAsync(id) ?? throw new KeyNotFoundException("Leave type not found.");
        db.LeaveTypes.Remove(type);
        await db.SaveChangesAsync();
    }
}
