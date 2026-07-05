using HrLeaves.Api.DTOs;
using HrLeaves.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaves.Api.Controllers;

[Route("api/leave-types")]
public class LeaveTypesController(ILeaveTypeService service) : ApiControllerBase
{
    [HttpGet] public async Task<IActionResult> Get() => Ok(await service.GetAsync());
    [HttpPost] public async Task<IActionResult> Create(UpsertLeaveTypeDto dto) { try { return Ok(await service.CreateAsync(dto)); } catch (Exception ex) { return HandleError(ex); } }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, UpsertLeaveTypeDto dto) { try { return Ok(await service.UpdateAsync(id, dto)); } catch (Exception ex) { return HandleError(ex); } }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) { try { await service.DeleteAsync(id); return NoContent(); } catch (Exception ex) { return HandleError(ex); } }
}
