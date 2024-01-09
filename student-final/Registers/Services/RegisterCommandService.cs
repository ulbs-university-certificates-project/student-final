using student_final.Registers.Models;
using student_final.Registers.Services.Interfaces;
using student_final.Students.Models;
using student_final.Students.Repository.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;
using student_final.Registers.DTOs;

namespace student_final.Registers.Services;

public class RegisterCommandService : IRegisterCommandService
{
    private Register _register;
    private IStudentRepository _studentRepository;

    public RegisterCommandService(Register register, IStudentRepository studentRepository)
    {
        _register = register;
        _studentRepository = studentRepository;
    }


    public async Task<CertificateObject> RequestCertificate(CertificateRequest request)
    {
        Student student = await _studentRepository.GetByNrMatricolAsync(request.NrMatricol);

        if (student == null)
        {
            throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
        }

        CertificateObject certificate = _register.CreateCertificate(student, request.Motiv);
        return certificate;
    }
}