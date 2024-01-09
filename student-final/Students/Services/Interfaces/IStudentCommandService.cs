using student_final.Students.Models;

namespace student_final.Students.Services.Interfaces;

public interface IStudentCommandService
{
    Task<Student> CreateStudent(Student student);
    Task<Student> UpdateStudent(Student student);
    Task DeleteStudent(int nrMatricol);
}