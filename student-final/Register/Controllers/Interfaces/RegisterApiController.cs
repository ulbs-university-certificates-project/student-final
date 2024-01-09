using Microsoft.AspNetCore.Mvc;
using student_final.Students.Models;

namespace student_final.Students.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class RegisterApiController:ControllerBase
{
    [HttpGet("post")]
    [ProducesResponseType(statusCode:200,type:typeof(String))]
    public abstract void RequestShowCells();
}