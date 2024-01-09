using student_final.Registers.Models;
using student_final.Registers.Services.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Registers.Services;

public class RegisterQueryService : IRegisterQueryService
{
    private Register _register;

    public RegisterQueryService(Register register)
    {
        _register = register;
    }

    public IEnumerable<CertificateObject> GetCertificates()
    {
        IEnumerable<CertificateObject> certificates = _register.GetCertificates();
        
        if (certificates.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_CERTIFICATES_EXIST);
        }

        return certificates;
    }

    public CertificateObject GetCertificateByNrAdeverinta(int nrAdeverinta)
    {
        CertificateObject certificate = _register.GetCertificateByNrAdeverinta(nrAdeverinta);

        if (certificate == null)
        {
            throw new ItemDoesNotExist(Constants.CERTIFICATE_DOES_NOT_EXIST);
        }

        return certificate;
    }
}