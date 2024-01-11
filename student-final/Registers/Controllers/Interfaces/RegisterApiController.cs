using Microsoft.AspNetCore.Mvc;
using student_final.Registers.DTOs;
using student_final.Registers.Models;

namespace student_final.Registers.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class RegisterApiController:ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Certificate>))]
    [ProducesResponseType(statusCode:404,type:typeof(string))]
    public abstract ActionResult<IEnumerable<Certificate>> GetCertificates();
    
    [HttpGet("certificate/{nrAdeverinta}")]
    [ProducesResponseType(statusCode:200,type:typeof(Certificate))]
    [ProducesResponseType(statusCode:404,type:typeof(string))]
    public abstract ActionResult<Certificate> GetCertificateByNrAdeverinta(int nrAdeverinta);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(Certificate))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    public abstract Task<ActionResult<Certificate>> RequestCertificate(CertificateRequest request);
}