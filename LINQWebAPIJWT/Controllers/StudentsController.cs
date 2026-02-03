using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LINQWebAPI.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize] // 🔐 PROTECTED
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new()
        {
            new Student { Id = 1, Name = "Ranjana", Age = 22 },
            new Student { Id = 2, Name = "Akhil", Age = 24 },
            new Student { Id = 3, Name = "Neha", Age = 21 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(students);
        }

        [HttpGet("adults")]
        public IActionResult Adults()
        {
            var adults = students.Where(s => s.Age >= 22).ToList();
            return Ok(adults);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            students.Remove(student);
            return Ok("Deleted");
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
