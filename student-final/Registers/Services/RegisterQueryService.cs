using student_final.Certificates.Models;
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

    public IEnumerable<Certificate> GetCertificates()
    {
        IEnumerable<Certificate> certificates = _register.GetCertificates();
        
        if (certificates.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_CERTIFICATES_EXIST);
        }

        return certificates;
    }

    public Certificate GetCertificateByNrAdeverinta(int nrAdeverinta)
    {
        Certificate certificate = _register.GetCertificateByNrAdeverinta(nrAdeverinta);

        if (certificate == null)
        {
            throw new ItemDoesNotExist(Constants.CERTIFICATE_DOES_NOT_EXIST);
        }

        return certificate;
    }
}