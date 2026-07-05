using HrLeaves.Api.DTOs;
namespace HrLeaves.Api.Services;
public interface ILeaveTypeService { Task<List<LeaveTypeDto>> GetAsync(); Task<LeaveTypeDto> CreateAsync(UpsertLeaveTypeDto dto); Task<LeaveTypeDto> UpdateAsync(int id, UpsertLeaveTypeDto dto); Task DeleteAsync(int id); }
