namespace HrLeaves.Api.Models;

public class LeaveBalance
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;
    public int LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; } = default!;
    public decimal TotalDays { get; set; }
    public decimal UsedDays { get; set; }
    public decimal RemainingDays => TotalDays - UsedDays;
}
