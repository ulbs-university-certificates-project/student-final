using Microsoft.AspNetCore.Mvc;
using student_final.Registers.Models;

namespace student_final.QR.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class QRApiController:ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(string))]
    public abstract ActionResult<string> GenerateAndSaveQRCode(Certificate certificate);
    
    [HttpDelete("delete/{qrName}")]
    [ProducesResponseType(statusCode:202,type:typeof(string))]
    public abstract ActionResult<string> GenerateAndSaveQRCode(string qrName);
}