namespace LINQWebAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        // Nullable to avoid warnings
        public string? Name { get; set; }

        public int Age { get; set; }
    }
}
