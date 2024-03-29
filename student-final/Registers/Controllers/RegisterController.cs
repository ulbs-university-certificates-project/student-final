﻿using Microsoft.AspNetCore.Mvc;
using student_final.Certificates.DTOs;
using student_final.Certificates.Models;
using student_final.Registers.Controllers.Interfaces;
using student_final.Registers.Models;
using student_final.Registers.Services.Interfaces;
using student_final.Students.Models;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Registers.Controllers;

public class RegisterController : RegisterApiController
{
    private IRegisterQueryService _queryService;
    private IRegisterCommandService _commandService;
    
    private ILogger<RegisterController> _logger;
    
    public RegisterController(IRegisterQueryService queryService, IRegisterCommandService commandService, ILogger<RegisterController> logger)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }

    public override ActionResult<IEnumerable<Certificate>> GetCertificates()
    {
        _logger.LogInformation("Rest request: Get all certificates.");
        try
        {
            IEnumerable<Certificate> certificates = _queryService.GetCertificates();

            return Ok(certificates);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override ActionResult<Certificate> GetCertificateByNrAdeverinta(int nrAdeverinta)
    {
        _logger.LogInformation($"Rest request: Get certificate with nr. adeverinta {nrAdeverinta}.");
        try
        {
            Certificate certificate = _queryService.GetCertificateByNrAdeverinta(nrAdeverinta);

            return Ok(certificate);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Certificate>> RequestCertificate(CertificateRequest request)
    {
        _logger.LogInformation($"Rest request: Request create certificate :\n{request}");
        try
        {
            Certificate response = await _commandService.RequestCertificate(request);

            return Created(Constants.CERTIFICATE_CREATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}