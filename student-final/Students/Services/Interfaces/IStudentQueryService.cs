using student_final.Students.Models;

namespace student_final.Students.Services.Interfaces;

public interface IStudentQueryService
{
    Task<IEnumerable<Student>> GetAllStudents();
    Task<Student> GetStudentByNrMatricol(int nrMatricol);
}