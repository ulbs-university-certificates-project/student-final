using Microsoft.AspNetCore.Mvc;
using student_final.Students.Models;

namespace student_final.Students.Controllers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class StudentApiController:ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Student>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    public abstract Task<ActionResult<IEnumerable<Student>>> GetAllStudents();

    [HttpGet("student/{nrMatricol}")]
    [ProducesResponseType(statusCode:200,type:typeof(Student))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    [Produces("application/json")]
    public abstract Task<ActionResult<Student>> GetStudentByNrMatricol([FromRoute]int nrMatricol);
    
    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(Student))]
    [ProducesResponseType(statusCode:409,type:typeof(String))]
    public abstract Task<ActionResult<Student>> CreateStudent(Student student);

    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(Student))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    public abstract Task<ActionResult<Student>> UpdateStudent(Student student);
    
    [HttpDelete("delete/{nrMatricol}")]
    [ProducesResponseType(statusCode:202,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    public abstract Task<ActionResult> DeleteStudent([FromRoute]int nrMatricol);
}