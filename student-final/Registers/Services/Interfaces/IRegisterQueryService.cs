using student_final.Registers.Models;

namespace student_final.Registers.Services.Interfaces;

public interface IRegisterQueryService
{
    IEnumerable<CertificateObject> GetCertificates();

    CertificateObject GetCertificateByNrAdeverinta(int nrAdeverinta);
}