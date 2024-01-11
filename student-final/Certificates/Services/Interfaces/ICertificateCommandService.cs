using student_final.Certificates.Models;

namespace student_final.Certificates.Services.Interfaces;

public interface ICertificateCommandService
{
    string CreateCertificateDocument(Certificate certificate);
}