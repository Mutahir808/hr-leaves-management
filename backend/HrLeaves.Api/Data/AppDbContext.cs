using HrLeaves.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HrLeaves.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
    public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
    public DbSet<LeaveSettlement> LeaveSettlements => Set<LeaveSettlement>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Employee>().HasIndex(x => x.Email).IsUnique();
        builder.Entity<LeaveType>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<LeaveBalance>().HasIndex(x => new { x.EmployeeId, x.LeaveTypeId }).IsUnique();
        builder.Entity<LeaveBalance>().Property(x => x.TotalDays).HasPrecision(10, 2);
        builder.Entity<LeaveBalance>().Property(x => x.UsedDays).HasPrecision(10, 2);
        builder.Entity<LeaveType>().Property(x => x.DefaultDays).HasPrecision(10, 2);
        builder.Entity<LeaveType>().Property(x => x.AccrualRatePerMonth).HasPrecision(10, 2);
        builder.Entity<LeaveRequest>().Property(x => x.DaysRequested).HasPrecision(10, 2);
        builder.Entity<LeaveSettlement>().Property(x => x.AdjustmentDays).HasPrecision(10, 2);
    }
}
