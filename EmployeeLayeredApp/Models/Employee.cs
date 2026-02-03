// namespace EmployeeLayeredApp.Models
// {
//     public class Employee
//     {
//         public int Id { get; set; }
//         public string? Name { get; set; }
//         public string? Email { get; set; }
//         public string? Department { get; set; }

//         public bool IsDeleted { get; set; } = false; // NEW
//     }
// }





using System.ComponentModel.DataAnnotations;

namespace EmployeeLayeredApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Department { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}









// using System.ComponentModel.DataAnnotations;
// using Microsoft.EntityFrameworkCore;

// namespace EmployeeLayeredApp.Models
// {
//     [Index(nameof(Email), IsUnique = true)] // ✅ UNIQUE EMAIL
//     public class Employee
//     {
//         public int Id { get; set; }

//         [Required]
//         public required string Name { get; set; }

//         [Required]
//         [EmailAddress]
//         public required string Email { get; set; }

//         public required string Department { get; set; }

//         public bool IsDeleted { get; set; } = false;
//     }
// }

// using System.ComponentModel.DataAnnotations;

// namespace EmployeeLayeredApp.Models
// {
//     public class Employee
//     {
//         public int Id { get; set; }

//         [Required]
//         public string Name { get; set; }

//         [EmailAddress] // ✅ Email format validation
//         public string Email { get; set; }

//         public string Department { get; set; }
//         public bool IsDeleted { get; set; } = false;
//     }
// }
