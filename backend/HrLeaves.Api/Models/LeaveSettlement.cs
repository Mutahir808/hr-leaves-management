namespace HrLeaves.Api.Models;

public class LeaveSettlement
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;
    public int LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; } = default!;
    public decimal AdjustmentDays { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
