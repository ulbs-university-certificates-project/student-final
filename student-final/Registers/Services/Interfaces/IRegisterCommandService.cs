using student_final.Registers.DTOs;
using student_final.Registers.Models;

namespace student_final.Registers.Services.Interfaces;

public interface IRegisterCommandService
{
    Task<Certificate> RequestCertificate(CertificateRequest request);
}