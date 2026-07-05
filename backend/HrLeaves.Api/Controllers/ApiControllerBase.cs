using Microsoft.AspNetCore.Mvc;

namespace HrLeaves.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult HandleError(Exception ex) => ex switch
    {
        KeyNotFoundException => NotFound(new { message = ex.Message }),
        InvalidOperationException => BadRequest(new { message = ex.Message }),
        _ => StatusCode(500, new { message = "Unexpected server error.", detail = ex.Message })
    };
}
