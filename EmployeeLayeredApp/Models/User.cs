// using System.ComponentModel.DataAnnotations;
// using Microsoft.EntityFrameworkCore;

// namespace EmployeeLayeredApp.Models
// {
//     [Index(nameof(Username), IsUnique = true)]
//     public class User
//     {
//         public int Id { get; set; }

//         public required string Username { get; set; }
//         public required string Password { get; set; }
//         public required string Role { get; set; } // Admin / User
//     }
// }
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLayeredApp.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; } // Admin / User
    }
}
