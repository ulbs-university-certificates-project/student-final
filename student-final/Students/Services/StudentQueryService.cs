using student_final.Students.Models;
using student_final.Students.Repository.Interfaces;
using student_final.Students.Services.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Students.Services;

public class StudentQueryService : IStudentQueryService
{
    private IStudentRepository _repository;
    
    public StudentQueryService(IStudentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Student>> GetAllStudents()
    {
        IEnumerable<Student> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_STUDENTS_EXIST);
        }

        return result;
    }

    public async Task<Student> GetStudentByNrMatricol(int nrMatricol)
    {
        Student result = await _repository.GetByNrMatricolAsync(nrMatricol);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
        }

        return result;
    }
}