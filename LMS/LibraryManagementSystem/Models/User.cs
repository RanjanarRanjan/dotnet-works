using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } // Auto increment

        [Required]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Username must contain only letters")]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        // [Phone]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile number must be exactly 10 digits")]
        public string ContactNumber { get; set; } = null!;

        [Required]
        public string Department { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public bool IsBlocked { get; set; } = false;
    }
}
