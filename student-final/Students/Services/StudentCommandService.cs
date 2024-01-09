using student_final.Students.Models;
using student_final.Students.Repository.Interfaces;
using student_final.Students.Services.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Students.Services;

public class StudentCommandService : IStudentCommandService
{
    private IStudentRepository _repository;
    
    public StudentCommandService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Student> CreateStudent(Student student)
    {
        if ( await _repository.GetByNrMatricolAsync(student.NrMatricol) != null)
        {
            throw new ItemAlreadyExists(Constants.STUDENT_ALREADY_EXISTS);
        }

        await _repository.CreateAsync(student);

        return student;
    }

    public async Task<Student> UpdateStudent(Student student)
    {
        if (await _repository.GetByNrMatricolAsync(student.NrMatricol) == null)
        {
            throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
        }

        await _repository.UpdateAsync(student);

        return student;
    }

    public async Task DeleteStudent(int nrMatricol)
    {
        Student student = await _repository.GetByNrMatricolAsync(nrMatricol);

        if (student == null)
        {
            throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
        }
            
        await _repository.DeleteAsync(nrMatricol);
    }
}