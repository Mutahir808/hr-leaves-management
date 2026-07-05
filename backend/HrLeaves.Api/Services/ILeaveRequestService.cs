using HrLeaves.Api.DTOs;

namespace HrLeaves.Api.Services;

public interface ILeaveRequestService
{
    Task<List<LeaveRequestDto>> GetAsync(string? status, int? leaveTypeId, DateOnly? fromDate, DateOnly? toDate);
    Task<LeaveRequestDto> CreateAsync(CreateLeaveRequestDto dto);
    Task<LeaveRequestDto> ApproveAsync(int id);
    Task<LeaveRequestDto> RejectAsync(int id, string? comment);
    Task BulkApproveAsync(List<int> ids);
    Task BulkRejectAsync(List<int> ids, string? comment);
    Task<string> ExportCsvAsync();
}
