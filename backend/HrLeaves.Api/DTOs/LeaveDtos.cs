using HrLeaves.Api.Models;

namespace HrLeaves.Api.DTOs;

public record LeaveTypeDto(int Id, string Name, decimal DefaultDays, bool IsAccrued, decimal AccrualRatePerMonth);
public record UpsertLeaveTypeDto(string Name, decimal DefaultDays, bool IsAccrued, decimal AccrualRatePerMonth);
public record LeaveBalanceDto(int Id, int EmployeeId, int LeaveTypeId, string LeaveTypeName, decimal TotalDays, decimal UsedDays, decimal RemainingDays);
public record CreateLeaveRequestDto(int EmployeeId, int LeaveTypeId, DateOnly StartDate, DateOnly EndDate, string Reason);
public record LeaveRequestDto(int Id, int EmployeeId, string EmployeeName, int LeaveTypeId, string LeaveTypeName, DateOnly StartDate, DateOnly EndDate, decimal DaysRequested, string Reason, LeaveRequestStatus Status, string? RejectionComment, DateTime CreatedAt);
public record RejectLeaveRequestDto(string? RejectionComment);
public record BulkActionDto(List<int> RequestIds, string? RejectionComment);
public record CreateSettlementDto(int EmployeeId, int LeaveTypeId, decimal AdjustmentDays, string Reason);
public record LeaveSettlementDto(int Id, int EmployeeId, string EmployeeName, int LeaveTypeId, string LeaveTypeName, decimal AdjustmentDays, string Reason, DateTime CreatedAt);
