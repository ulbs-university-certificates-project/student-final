using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace student_final.Students.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("nr_matricol")]
    public int NrMatricol { get; set; }
    
    [Required]
    [Column("nume")]
    public string Nume { get; set; }
    
    [Required]  
    [Column("an")]
    public int An { get; set; }
    
    [Required]
    [Column("sectie")]
    public string Sectie { get; set; }
}