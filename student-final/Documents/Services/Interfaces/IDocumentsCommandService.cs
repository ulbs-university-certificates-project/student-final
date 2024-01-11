using student_final.Certificates.Models;

namespace student_final.Documents.Services.Interfaces;

public interface IDocumentsCommandService
{
    string CreateCertificateDocument(Certificate certificate);
}