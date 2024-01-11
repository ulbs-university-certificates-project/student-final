using Microsoft.AspNetCore.Mvc;
using student_final.Certificates.Models;

namespace student_final.Certificates.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class CertificatesApiController:ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(string))]
    public abstract ActionResult<string> CreateCertificateDocument(Certificate certificate);
    
}