using Microsoft.AspNetCore.Mvc;
using student_final.Register.Services.Interfaces;
using student_final.Students.Controllers.Interfaces;
using student_final.Students.Models;
using student_final.Students.Services.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Students.Controllers;

public class RegisterController : RegisterApiController
{
    private IRegisterService _service;
    
    private ILogger<RegisterController> _logger;
    
    public RegisterController(IRegisterService service, ILogger<RegisterController> logger)
    {
        _service = service;
        _logger = logger;
    }

    public override void RequestShowCells()
    {
        Ok(_service.LogCells());
    }
}