using HrLeaves.Api.DTOs;
using HrLeaves.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaves.Api.Controllers;

[Route("api/leave-requests")]
public class LeaveRequestsController(ILeaveRequestService service) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<LeaveRequestDto>), 200)]
    public async Task<IActionResult> Get([FromQuery] string? status, [FromQuery] int? leaveTypeId, [FromQuery] DateOnly? fromDate, [FromQuery] DateOnly? toDate)
        => Ok(await service.GetAsync(status, leaveTypeId, fromDate, toDate));

    [HttpGet("pending")]
    public async Task<IActionResult> Pending() => Ok(await service.GetAsync("Pending", null, null, null));

    [HttpPost]
    [ProducesResponseType(typeof(LeaveRequestDto), 200)]
    [ProducesResponseType(typeof(object), 400)]
    public async Task<IActionResult> Create(CreateLeaveRequestDto dto) { try { return Ok(await service.CreateAsync(dto)); } catch (Exception ex) { return HandleError(ex); } }

    [HttpPost("{id:int}/approve")]
    public async Task<IActionResult> Approve(int id) { try { return Ok(await service.ApproveAsync(id)); } catch (Exception ex) { return HandleError(ex); } }

    [HttpPost("{id:int}/reject")]
    public async Task<IActionResult> Reject(int id, RejectLeaveRequestDto dto) { try { return Ok(await service.RejectAsync(id, dto.RejectionComment)); } catch (Exception ex) { return HandleError(ex); } }

    [HttpPost("bulk-approve")]
    public async Task<IActionResult> BulkApprove(BulkActionDto dto) { try { await service.BulkApproveAsync(dto.RequestIds); return Ok(new { message = "Selected requests approved." }); } catch (Exception ex) { return HandleError(ex); } }

    [HttpPost("bulk-reject")]
    public async Task<IActionResult> BulkReject(BulkActionDto dto) { try { await service.BulkRejectAsync(dto.RequestIds, dto.RejectionComment); return Ok(new { message = "Selected requests rejected." }); } catch (Exception ex) { return HandleError(ex); } }

    [HttpGet("export")]
    public async Task<IActionResult> Export() => File(System.Text.Encoding.UTF8.GetBytes(await service.ExportCsvAsync()), "text/csv", "leave-history.csv");
}
