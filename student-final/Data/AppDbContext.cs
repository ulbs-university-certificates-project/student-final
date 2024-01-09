using Microsoft.EntityFrameworkCore;
using student_final.Students.Models;

namespace student_final.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }
}