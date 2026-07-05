using HrLeaves.Api.DTOs;
using HrLeaves.Api.Models;

namespace HrLeaves.Api.Services;

public static class LeaveMapping
{
    public static LeaveTypeDto ToDto(this LeaveType x) => new(x.Id, x.Name, x.DefaultDays, x.IsAccrued, x.AccrualRatePerMonth);
    public static LeaveBalanceDto ToDto(this LeaveBalance x) => new(x.Id, x.EmployeeId, x.LeaveTypeId, x.LeaveType.Name, x.TotalDays, x.UsedDays, x.RemainingDays);
    public static LeaveRequestDto ToDto(this LeaveRequest x) => new(x.Id, x.EmployeeId, x.Employee.FullName, x.LeaveTypeId, x.LeaveType.Name, x.StartDate, x.EndDate, x.DaysRequested, x.Reason, x.Status, x.RejectionComment, x.CreatedAt);
    public static LeaveSettlementDto ToDto(this LeaveSettlement x) => new(x.Id, x.EmployeeId, x.Employee.FullName, x.LeaveTypeId, x.LeaveType.Name, x.AdjustmentDays, x.Reason, x.CreatedAt);
}
