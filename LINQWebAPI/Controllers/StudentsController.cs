using Microsoft.AspNetCore.Mvc;
using LINQWebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace LINQWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // Sample in-memory data
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Ranjana", Age = 22 },
            new Student { Id = 2, Name = "Akhil", Age = 24 },
            new Student { Id = 3, Name = "Neha", Age = 21 },
            new Student { Id = 4, Name = "Rahul", Age = 22 },
            new Student { Id = 5, Name = "Anita", Age = 24 }
        };

        // GET: api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(students);
        }

        // GET: api/students/adults
        [HttpGet("adults")]
        public IActionResult GetAdults()
        {
            var adults = students.Where(s => s.Age >= 22).ToList();
            return Ok(adults);
        }

        // GET: api/students/names
        [HttpGet("names")]
        public IActionResult GetNames()
        {
            var names = students.Select(s => s.Name).ToList();
            return Ok(names);
        }

        // GET: api/students/sorted
        [HttpGet("sorted")]
        public IActionResult GetSortedByAge()
        {
            var sorted = students.OrderBy(s => s.Age).ToList();
            return Ok(sorted);
        }

        // GET: api/students/grouped
        [HttpGet("grouped")]
        public IActionResult GetGroupedByAge()
        {
            var grouped = students.GroupBy(s => s.Age)
                                  .Select(g => new
                                  {
                                      Age = g.Key,
                                      Students = g.Select(s => s.Name)
                                  })
                                  .ToList();
            return Ok(grouped);
        }
    }
}
