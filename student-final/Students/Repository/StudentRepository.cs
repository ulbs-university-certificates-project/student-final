using AutoMapper;
using Microsoft.EntityFrameworkCore;
using student_final.Data;
using student_final.Students.Models;
using student_final.Students.Repository.Interfaces;

namespace student_final.Students.Repository;

public class StudentRepository : IStudentRepository
{
    private AppDbContext _context;
    private IMapper _mapper;

    public StudentRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetByNrMatricolAsync(int nrMatricol)
    {
        return (await _context.Students.FirstOrDefaultAsync(student => student.NrMatricol == nrMatricol))!;
    }

    public async Task<Student> CreateAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return student;
    }
    
    public async Task DeleteAsync(int nrMatricol)
    {
        Student student = (await _context.Students.FindAsync(nrMatricol))!;
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}