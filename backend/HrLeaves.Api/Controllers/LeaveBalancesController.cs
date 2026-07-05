using HrLeaves.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrLeaves.Api.Controllers;

[Route("api/leave-balances")]
public class LeaveBalancesController(ILeaveBalanceService service) : ApiControllerBase
{
    [HttpGet("employee/{employeeId:int}")]
    public async Task<IActionResult> GetByEmployee(int employeeId) => Ok(await service.GetByEmployeeAsync(employeeId));
}
