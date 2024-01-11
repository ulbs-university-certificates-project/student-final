using Microsoft.AspNetCore.Mvc;
using student_final.QR.Controllers.Interfaces;
using student_final.QR.Services.Interfaces;
using student_final.Registers.Models;
using student_final.System.Constants;

namespace student_final.QR.Controllers;

public class QRController : QRApiController
{
    private IQRCommandService _commandService;

    public QRController(IQRCommandService commandService)
    {
        _commandService = commandService;
    }
    
    public override ActionResult<string> GenerateAndSaveQRCode(Certificate certificate)
    {
        string name = _commandService.GenerateAndSaveQRCode(certificate);

        return Ok(Constants.QR_CODE_CREATED + $" : {name}");
    }

    public override ActionResult<string> GenerateAndSaveQRCode(string qrName)
    {
        _commandService.DeleteQRCode(qrName);

        return Ok(Constants.QR_CODE_DELETED);
    }
}