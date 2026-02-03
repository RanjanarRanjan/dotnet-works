// using LibraryManagementSystem.Data;
// using LibraryManagementSystem.Models;
// using Microsoft.EntityFrameworkCore;

// namespace LibraryManagementSystem.Repositories
// {
//     public class BookRepository : IBookRepository
//     {
//         private readonly AppDbContext _context;

//         public BookRepository(AppDbContext context)
//         {
//             _context = context;
//         }

//         // Add book (duplicate check)
//         public async Task<bool> BookExists(string bookName, string author)
//         {
//             return await _context.Books.AnyAsync(b =>
//                 b.BookName == bookName &&
//                 b.Author == author &&
//                 !b.IsDeleted);
//         }

//         public async Task<Book?> AddBook(Book book)
//         {
//     //  Validate TotalCopies > 0
//             if (book.TotalCopies <= 0)
//                 return null;

//     //  Ensure AvailableCopies = TotalCopies
//             book.AvailableCopies = book.TotalCopies;

//     //  Prevent duplicate BookName + Author
//             if (await BookExists(book.BookName, book.Author))
//                 return null;

//             _context.Books.Add(book);
//             await _context.SaveChangesAsync();
//             return book;
//         }


//         public async Task<Book?> UpdateBook(Book book)
//         {
//             _context.Books.Update(book);
//             await _context.SaveChangesAsync();
//             return book;
//         }

//         public async Task<bool> DeleteBook(int id)
//         {
//             var book = await GetBookById(id);
//             if (book == null) return false;

//             book.IsDeleted = true;
//             await _context.SaveChangesAsync();
//             return true;
//         }

//         public async Task<Book?> GetBookById(int id)
//         {
//             return await _context.Books
//                 .FirstOrDefaultAsync(b => b.BookID == id); // include deleted
//         }

//         // Search books by name, author, or ID
//       // Get a single book by ID (with updated AvailableCopies)
//         public async Task<Book?> GetBookByIdSimple(int bookId)
//         {
//             return await _context.Books
//                 .FirstOrDefaultAsync(b => b.BookID == bookId && !b.IsDeleted);
//         }


//         public async Task<List<Book>> SearchBooks(int? bookId, string? bookName, string? author)
//         {
//             var query = _context.Books.AsQueryable();

//             // Filter by BookID if provided
//             if (bookId.HasValue)
//                 query = query.Where(b => b.BookID == bookId.Value);

//             // Filter by BookName if provided
//             if (!string.IsNullOrEmpty(bookName))
//                 query = query.Where(b => b.BookName.Contains(bookName));

//             // Filter by Author if provided
//             if (!string.IsNullOrEmpty(author))
//                 query = query.Where(b => b.Author.Contains(author));

//             // Only include active (not deleted) books
//             var books = await query.Where(b => !b.IsDeleted).ToListAsync();

//             // Calculate real-time available copies
//             foreach (var book in books)
//             {
//                 var issuedCount = await _context.BookIssues
//                     .Where(i => i.BookId == book.BookID && i.ReturnStatus == "Not Returned")
//                     .CountAsync();

//                 book.AvailableCopies = book.TotalCopies - issuedCount;
//             }

//         return books;
//         }



//         // 🔹 Get all books including deleted
//         public async Task<(List<Book>, int)> GetAllBooksPaged(int pageNumber, int pageSize)
//         {
//             var query = _context.Books.AsQueryable(); // include deleted

//             var total = await query.CountAsync();
//             var books = await query
//                 .OrderBy(b => b.BookID)
//                 .Skip((pageNumber - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToListAsync();

//             return (books, total);
//         }

//         // 🔹 Get active books only (not deleted, AvailableCopies > 0)
//         public async Task<(List<Book>, int)> GetActiveBooksPaged(int pageNumber, int pageSize)
//         {
//             var query = _context.Books
//                 .Where(b => !b.IsDeleted && b.AvailableCopies > 0);

//             var total = await query.CountAsync();
//             var books = await query
//                 .OrderBy(b => b.BookID)
//                 .Skip((pageNumber - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToListAsync();

//             return (books, total);
//         }
//     }
// }

using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BookExists(string bookName, string author)
        {
            return await _context.Books.AnyAsync(b =>
                b.BookName == bookName &&
                b.Author == author &&
                !b.IsDeleted);
        }

        // ✅ ADD BOOK WITH CATEGORY
        public async Task<Book?> AddBookWithCategories(AddBookDto dto)
        {
            if (dto.TotalCopies <= 0)
                return null;

            if (await BookExists(dto.BookName, dto.Author))
                return null;

            var book = new Book
            {
                BookName = dto.BookName,
                Author = dto.Author,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.TotalCopies
            };

            foreach (var catId in dto.CategoryIds)
            {
                book.BookCategories.Add(new BookCategory
                {
                    CategoryId = catId
                });
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await GetBookById(id);
            if (book == null) return false;

            book.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.BookID == id);
        }

        // ✅ SEARCH (NO RECALCULATION BUG)
        public async Task<List<Book>> SearchBooks(int? bookId, string? bookName, string? author)
        {
            var query = _context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Where(b => !b.IsDeleted);

            if (bookId.HasValue)
                query = query.Where(b => b.BookID == bookId);

            if (!string.IsNullOrEmpty(bookName))
                query = query.Where(b => b.BookName.Contains(bookName));

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author.Contains(author));

            return await query.ToListAsync();
        }

        // ✅ SEARCH BY CATEGORY
        public async Task<List<Book>> SearchByCategory(string category)
        {
            return await _context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Where(b => !b.IsDeleted &&
                    b.BookCategories.Any(bc =>
                        bc.Category.CategoryName.Contains(category)))
                .ToListAsync();
        }

        public async Task<(List<Book>, int)> GetAllBooksPaged(int pageNumber, int pageSize)
        {
            var query = _context.Books.AsQueryable();
            var total = await query.CountAsync();

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, total);
        }

        public async Task<(List<Book>, int)> GetActiveBooksPaged(int pageNumber, int pageSize)
        {
            var query = _context.Books
                .Where(b => !b.IsDeleted && b.AvailableCopies > 0);

            var total = await query.CountAsync();
            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, total);
        }
    }
}

