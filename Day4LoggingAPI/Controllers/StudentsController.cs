using Microsoft.AspNetCore.Mvc;
using Day4LoggingAPI.Models;

namespace Day4LoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new()
        {
            new Student { Id = 1, Name = "Akhil", Marks = 85 },
            new Student { Id = 2, Name = "Neha", Marks = 72 },
            new Student { Id = 3, Name = "Ranjana", Marks = 90 },
            new Student { Id = 4, Name = "Manu", Marks = 60 }
        };

        // LINQ - Where & Select
        [HttpGet("passed")]
        public IActionResult GetPassedStudents()
        {
            var result = students
                         .Where(s => s.Marks >= 70)
                         .Select(s => new { s.Name, s.Marks });

            return Ok(result);
        }

        // LINQ - OrderBy
        [HttpGet("topper")]
        public IActionResult GetTopper()
        {
            var topper = students
                         .OrderByDescending(s => s.Marks)
                         .First();

            return Ok(topper);
        }
    }
}
