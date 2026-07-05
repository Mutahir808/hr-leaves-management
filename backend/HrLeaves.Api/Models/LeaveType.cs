namespace HrLeaves.Api.Models;

public class LeaveType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DefaultDays { get; set; }
    public bool IsAccrued { get; set; }
    public decimal AccrualRatePerMonth { get; set; }
    public ICollection<LeaveBalance> LeaveBalances { get; set; } = [];
    public ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
}
