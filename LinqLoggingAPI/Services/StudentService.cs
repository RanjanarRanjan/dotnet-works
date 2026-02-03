using LinqLoggingAPI.Models;

namespace LinqLoggingAPI.Services
{
    public class StudentService
    {
        private List<Student> students = new()
        {
            new Student { Id = 1, Name = "Akhil", Age = 21, Course = "CSE", Marks = 85 },
            new Student { Id = 2, Name = "Neha", Age = 22, Course = "ECE", Marks = 72 },
            new Student { Id = 3, Name = "Ranjana", Age = 23, Course = "CSE", Marks = 90 },
            new Student { Id = 4, Name = "Manu", Age = 20, Course = "ME", Marks = 65 }
        };

        // LINQ: Read All
        public List<Student> GetAllStudents()
        {
            return students;
        }

        // LINQ: Where
        public List<Student> GetCseStudents()
        {
            return students.Where(s => s.Course == "CSE").ToList();
        }

        // LINQ: OrderBy
        public List<Student> OrderByMarks()
        {
            return students.OrderByDescending(s => s.Marks).ToList();
        }

        // LINQ: Select
        public List<string> GetStudentNames()
        {
            return students.Select(s => s.Name).ToList();
        }
    }
}
