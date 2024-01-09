using Microsoft.AspNetCore.Mvc;
using student_final.Students.Controllers.Interfaces;
using student_final.Students.Models;
using student_final.Students.Services.Interfaces;
using student_final.System.Constants;
using student_final.System.Exceptions;

namespace student_final.Students.Controllers;

public class StudentsController : StudentApiController
{
    private IStudentQueryService _queryService;
    private IStudentCommandService _commandService;

    private ILogger<StudentsController> _logger;
    
    public StudentsController(IStudentQueryService queryService, IStudentCommandService commandService, ILogger<StudentsController> logger)
    {
        _queryService = queryService;
        _commandService = commandService;
        _logger = logger;
    }
    
    public override async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
    {
        _logger.LogInformation("Rest request: Get all students.");
        try
        {
            IEnumerable<Student> result = await _queryService.GetAllStudents();
            
            return Ok(result);
        }
        catch (ItemsDoNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Student>> GetStudentByNrMatricol(int nrMatricol)
    {
        _logger.LogInformation($"Rest request: Get student with nr. matricol {nrMatricol}.");
        try
        {
            Student result = await _queryService.GetStudentByNrMatricol(nrMatricol);

            return Ok(result);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        _logger.LogInformation($"Rest request: Create student :\n{student}");
        try
        {
            Student response = await _commandService.CreateStudent(student);

            return Created(Constants.STUDENT_CREATED, response);
        }
        catch (ItemAlreadyExists ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return Conflict(ex.Message);
        }
    }

    public override async Task<ActionResult<Student>> UpdateStudent(Student student)
    {
        _logger.LogInformation($"Rest request: Update student :\n{student}");
        try
        {
            Student response = await _commandService.UpdateStudent(student);

            return Accepted(Constants.STUDENT_UPDATED, response);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult> DeleteStudent(int nrMatricol)
    {
        _logger.LogInformation($"Rest request: Delete student with nr. matricol {nrMatricol}");
        try
        {
            await _commandService.DeleteStudent(nrMatricol);

            return Accepted(Constants.STUDENT_DELETED, Constants.STUDENT_DELETED);
        }
        catch (ItemDoesNotExist ex)
        {
            _logger.LogInformation($"Rest response: {ex.Message}");
            return NotFound(ex.Message);
        }
    }
}