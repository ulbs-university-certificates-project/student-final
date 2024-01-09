using student_final.Students.Models;

namespace student_final.Students.Repository.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student> GetByNrMatricolAsync(int nrMatricol);
    Task<Student> CreateAsync(Student student);
    Task<Student> UpdateAsync(Student student);
    Task DeleteAsync(int nrMatricol);
}