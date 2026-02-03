using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repo;

        public BookController(IBookRepository repo)
        {
            _repo = repo;
        }

        // =====================
        // ADD BOOK (ADMIN)
        // =====================
//         [Authorize(Roles = "Admin")]
// [HttpPost("add")]
// public async Task<IActionResult> AddBook([FromBody] Book book)
// {
//     if (book.TotalCopies <= 0)
//         return BadRequest("TotalCopies must be greater than 0");

//     var created = await _repo.AddBook(book);

//     if (created == null)
//         return BadRequest("Book already exists or invalid data");

//     return Ok(created);
// }

        // =====================
        // UPDATE BOOK (ADMIN)
        // =====================
        [Authorize(Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            var existing = await _repo.GetBookById(id);
            if (existing == null) return NotFound();

            existing.BookName = book.BookName;
            existing.Author = book.Author;
            existing.TotalCopies = book.TotalCopies;
            existing.AvailableCopies = book.AvailableCopies;

            await _repo.UpdateBook(existing);
            return Ok("Book updated");
        }

        // =====================
        // DELETE BOOK (ADMIN)
        // =====================
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _repo.DeleteBook(id);
            if (!deleted) return NotFound();

            return Ok("Book deleted");
        }

        // =====================
        // GET ALL BOOKS
        // =====================
         [Authorize(Roles = "Admin")]
[HttpGet("all")]
public async Task<IActionResult> GetAllBooks(
    int pageNumber = 1,
    int pageSize = 5)
{
    var (books, total) = await _repo.GetAllBooksPaged(pageNumber, pageSize);

    return Ok(new
    {
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalBooks = total,
        Data = books
    });
}

        // =====================
        // GET ACTIVE BOOKS
        // =====================
        [Authorize]
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveBooks(
            int pageNumber = 1,
            int pageSize = 5)
        {
            var (books, total) =
                await _repo.GetActiveBooksPaged(pageNumber, pageSize);

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalBooks = total,
                Data = books
            });
        }

        // =====================
        // SEARCH BOOK
        // =====================
//       [Authorize]
// [HttpGet("search")]
// public async Task<IActionResult> SearchBook(
//     int? bookId,
//     string? bookName,
//     string? author)
// {
//     var books = await _repo.SearchBooks(bookId, bookName, author);
//     return Ok(books);
// }










        // ✅ ADD BOOK WITH CATEGORY
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddBook(AddBookDto dto)
        {
            var book = await _repo.AddBookWithCategories(dto);

            if (book == null)
                return BadRequest("Invalid data or duplicate book");

            return Ok(book);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchBook(
            int? bookId,
            string? bookName,
            string? author)
        {
            return Ok(await _repo.SearchBooks(bookId, bookName, author));
        }

        [Authorize]
        [HttpGet("search-by-category")]
        public async Task<IActionResult> SearchByCategory(string category)
        {
            return Ok(await _repo.SearchByCategory(category));
        }

    }
}
