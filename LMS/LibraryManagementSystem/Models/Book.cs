// using System.ComponentModel.DataAnnotations;

// namespace LibraryManagementSystem.Models
// {
//     public class Book
//     {
//         [Key]
//         public int BookID { get; set; }

//         [Required]
//         [MaxLength(100)]
//         public string BookName { get; set; } = null!;

//         [Required]
//         [MaxLength(100)]
//         public string Author { get; set; } = null!;

//         [Range(1, int.MaxValue)]
//         public int TotalCopies { get; set; }

//         [Range(0, int.MaxValue)]
//         public int AvailableCopies { get; set; }

//         public bool IsDeleted { get; set; } = false;
//     }
// }
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [MaxLength(100)]
        public string BookName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Author { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int TotalCopies { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }

        public bool IsDeleted { get; set; } = false;

        // ✅ CATEGORY RELATION
        public ICollection<BookCategory> BookCategories { get; set; }
            = new List<BookCategory>();
    }
}
