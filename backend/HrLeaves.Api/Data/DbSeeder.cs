using HrLeaves.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HrLeaves.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Employees.AnyAsync()) return;

        var employee = new Employee { FullName = "Demo Employee", Email = "employee@example.com", HireDate = new DateOnly(2025, 1, 1) };
        var types = new[]
        {
            new LeaveType { Name = "Vacation", DefaultDays = 15, IsAccrued = true, AccrualRatePerMonth = 1.25m },
            new LeaveType { Name = "Sick", DefaultDays = 10, IsAccrued = false, AccrualRatePerMonth = 0 },
            new LeaveType { Name = "Maternity", DefaultDays = 90, IsAccrued = false, AccrualRatePerMonth = 0 }
        };

        db.Employees.Add(employee);
        db.LeaveTypes.AddRange(types);
        await db.SaveChangesAsync();

        foreach (var type in types)
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
    }
}
