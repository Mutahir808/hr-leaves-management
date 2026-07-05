using HrLeaves.Api.Data;
using HrLeaves.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HrLeaves.Api.Services;

public class LeaveBalanceService(AppDbContext db) : ILeaveBalanceService
{
    public async Task<List<LeaveBalanceDto>> GetByEmployeeAsync(int employeeId) => await db.LeaveBalances
        .Include(x => x.LeaveType)
        .Where(x => x.EmployeeId == employeeId)
        .Select(x => x.ToDto())
        .ToListAsync();
}
