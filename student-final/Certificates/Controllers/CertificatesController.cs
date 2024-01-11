using Microsoft.AspNetCore.Mvc;
using student_final.Certificates.Controllers.Interfaces;
using student_final.Certificates.Models;
using student_final.Certificates.Services.Interfaces;
using student_final.System.Constants;

namespace student_final.Certificates.Controllers;

public class CertificatesController : CertificatesApiController
{
    private ICertificateCommandService _commandService;

    public CertificatesController(ICertificateCommandService commandService)
    {
        _commandService = commandService;
    }

    public override ActionResult<string> CreateCertificateDocument(Certificate certificate)
    {
        string certificateName = _commandService.CreateCertificateDocument(certificate);

        return Ok(Constants.CERTIFICATE_DOCUMENT_CREATED + $" : ${certificateName}");
    }
}