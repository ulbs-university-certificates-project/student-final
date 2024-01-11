using student_final.Registers.Models;

namespace student_final.Registers.Services.Interfaces;

public interface IRegisterQueryService
{
    IEnumerable<Certificate> GetCertificates();

    Certificate GetCertificateByNrAdeverinta(int nrAdeverinta);
}