using HrLeaves.Api.DTOs;
namespace HrLeaves.Api.Services;
public interface ILeaveSettlementService { Task<List<LeaveSettlementDto>> GetAsync(); Task<LeaveSettlementDto> CreateAsync(CreateSettlementDto dto); }
