using Microsoft.AspNetCore.Mvc;
using student_final.Certificates.Controllers.Interfaces;
using student_final.Certificates.DTOs;
using student_final.Certificates.Models;
using student_final.Documents.Services.Interfaces;
using student_final.Emails.Services.Interfaces;
using student_final.Registers.Services.Interfaces;
using student_final.System.Constants;

namespace student_final.Certificates.Controllers;

public class CertificatesController : CertificatesApiController
{
    private IDocumentsCommandService _documentsCommandService;
    private IRegisterCommandService _registerCommandService;
    private IEmailSenderCommandService _emailSenderCommandService;

    public CertificatesController(IDocumentsCommandService documentsCommandService,
        IRegisterCommandService registerCommandService,
        IEmailSenderCommandService emailSenderCommandService)
    {
        _documentsCommandService = documentsCommandService;
        _registerCommandService = registerCommandService;
        _emailSenderCommandService = emailSenderCommandService;
    }

    public override async Task<ActionResult<string>> RequestUserCertificate(CertificateRequest request)
    {
        Certificate certificate = await _registerCommandService.RequestCertificate(request);
        string certificateName = _documentsCommandService.CreateCertificateDocument(certificate);

        await _emailSenderCommandService.SendEmailAsync(certificateName);
        // _documentsCommandService.DeleteCertificateDocument(certificateName);
        return Ok(Constants.EMAIL_SENT);
    }
}