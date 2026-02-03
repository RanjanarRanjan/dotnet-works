using LinqLoggingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LinqLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentsController(StudentService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            Log.Information("Fetching all students");
            return Ok(_service.GetAllStudents());
        }

        [HttpGet("cse")]
        public IActionResult GetCseStudents()
        {
            Log.Information("Fetching CSE students");
            return Ok(_service.GetCseStudents());
        }

        [HttpGet("orderby-marks")]
        public IActionResult OrderByMarks()
        {
            Log.Information("Ordering students by marks");
            return Ok(_service.OrderByMarks());
        }

        [HttpGet("names")]
        public IActionResult GetNames()
        {
            Log.Information("Fetching student names");
            return Ok(_service.GetStudentNames());
        }

        // To test exception
        [HttpGet("error")]
        public IActionResult ThrowError()
        {
            throw new Exception("Test exception");
        }
    }
}
