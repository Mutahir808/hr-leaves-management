using HrLeaves.Api.DTOs;
using HrLeaves.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaves.Api.Controllers;

[Route("api/leave-settlements")]
public class LeaveSettlementsController(ILeaveSettlementService service) : ApiControllerBase
{
    [HttpGet] public async Task<IActionResult> Get() => Ok(await service.GetAsync());
    [HttpPost] public async Task<IActionResult> Create(CreateSettlementDto dto) { try { return Ok(await service.CreateAsync(dto)); } catch (Exception ex) { return HandleError(ex); } }
}
