using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<BookCategory> BookCategories { get; set; }
            = new List<BookCategory>();
    }
}
