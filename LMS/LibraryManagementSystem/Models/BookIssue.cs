using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class BookIssue
    {
        [Key]
        public int IssueId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public string BookName { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime ReturnDate { get; set; } = DateTime.UtcNow.AddDays(20);

        [Required]
        public string ReturnStatus { get; set; } = "Not Returned";
    }
}
