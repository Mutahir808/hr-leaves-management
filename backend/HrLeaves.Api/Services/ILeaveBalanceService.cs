using HrLeaves.Api.DTOs;
namespace HrLeaves.Api.Services;
public interface ILeaveBalanceService { Task<List<LeaveBalanceDto>> GetByEmployeeAsync(int employeeId); }
