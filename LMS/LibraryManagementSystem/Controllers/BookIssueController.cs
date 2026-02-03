// using LibraryManagementSystem.Repositories;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace LibraryManagementSystem.Controllers
// {
//     [Route("api/bookissue")]
//     [ApiController]
//     public class BookIssueController : ControllerBase
//     {
//         private readonly IBookIssueRepository _repo;

//         public BookIssueController(IBookIssueRepository repo)
//         {
//             _repo = repo;
//         }

//         // ISSUE BOOK (Admin/User)
//         [Authorize]
// [HttpPost("issue")]
// public async Task<IActionResult> IssueBook(int bookId, int userId)
// {
//     var result = await _repo.IssueBook(bookId, userId);

//     if (result == null)
//         return BadRequest("Invalid book or user, or book unavailable");

//     return Ok(result);
// }

//         // RETURN BOOK (ADMIN ONLY)
//         [Authorize(Roles = "Admin")]
//         [HttpPatch("return/{issueId}")]
//         public async Task<IActionResult> ReturnBook(int issueId)
//         {
//             var result = await _repo.MarkAsReturned(issueId);
//             if (result == null)
//                 return BadRequest("Invalid issue");

//             return Ok("Book returned successfully");
//         }

//         // SEARCH ISSUED BOOKS
//         [Authorize]
//         [HttpGet("search")]
//         public async Task<IActionResult> SearchIssuedBooks(
//             string keyword, int pageNumber = 1, int pageSize = 5)
//         {
//             var (data, total) =
//                 await _repo.GetIssuedBooksByBook(keyword, pageNumber, pageSize);

//             return Ok(new { total, data });
//         }

//         // USER BORROWED BOOKS
//         [Authorize]
//         [HttpGet("user/{userId}")]
//         public async Task<IActionResult> UserBorrowedBooks(
//             int userId, int pageNumber = 1, int pageSize = 5)
//         {
//             var (data, total) =
//                 await _repo.GetBorrowedBooksByUser(userId, pageNumber, pageSize);

//             return Ok(new { total, data });
//         }

        
//         [Authorize]
// [HttpGet("me")]
// public async Task<IActionResult> GetMyBorrowedBooks([FromQuery] int pageNumber = 1,
//                                                     [FromQuery] int pageSize = 5)
// {
//     var userIdClaim = User.FindFirst("UserID")?.Value;
//     if (string.IsNullOrEmpty(userIdClaim))
//         return Unauthorized("Invalid token");

//     int userId = int.Parse(userIdClaim);

//     var (books, totalCount) = await _repo.GetBorrowedBooksByUser(userId, pageNumber, pageSize);

//     return Ok(new
//     {
//         PageNumber = pageNumber,
//         PageSize = pageSize,
//         TotalCount = totalCount,
//         TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
//         Data = books
//     });
// }

//     }
// }
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/bookissue")]
    public class BookIssueController : ControllerBase
    {
        private readonly IBookIssueRepository _repo;

        public BookIssueController(IBookIssueRepository repo)
        {
            _repo = repo;
        }

        // ISSUE BOOK
        [Authorize]
        [HttpPost("issue")]
        public async Task<IActionResult> IssueBook(int bookId, int userId)
        {
            var result = await _repo.IssueBook(bookId, userId);
            if (result == null)
                return BadRequest("Invalid book or user, or book unavailable");

            return Ok(result);
        }

        // RETURN BOOK (ADMIN)
        [Authorize(Roles = "Admin")]
        [HttpPatch("return/{issueId}")]
        public async Task<IActionResult> ReturnBook(int issueId)
        {
            var result = await _repo.MarkAsReturned(issueId);
            if (result == null)
                return BadRequest("Invalid issue");

            return Ok("Book returned successfully");
        }

        // GET ALL ISSUED BOOKS
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllIssuedBooks()
        {
            return Ok(await _repo.GetAllIssuedBooks());
        }

        // GET RETURNED BOOKS
        [Authorize(Roles = "Admin")]
        [HttpGet("returned")]
        public async Task<IActionResult> GetReturnedBooks()
        {
            return Ok(await _repo.GetReturnedBooks());
        }

        // GET NOT RETURNED BOOKS
        [Authorize(Roles = "Admin")]
        [HttpGet("not-returned")]
        public async Task<IActionResult> GetNotReturnedBooks()
        {
            return Ok(await _repo.GetNotReturnedBooks());
        }

        // GET ISSUED BOOKS BY BOOK ID
        [Authorize]
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetIssuedBooksByBookId(int bookId)
        {
            return Ok(await _repo.GetIssuedBooksByBookId(bookId));
        }

        // USER – MY BORROWED BOOKS
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyBorrowedBooks(
            int pageNumber = 1, int pageSize = 5)
        {
            var userIdClaim = User.FindFirst("UserID")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Invalid token");

            int userId = int.Parse(userIdClaim);

            var (data, total) =
                await _repo.GetBorrowedBooksByUser(userId, pageNumber, pageSize);

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Data = data
            });
        }

         [Authorize(Roles = "Admin")]
[HttpGet("issue/{issueId}")]
public async Task<IActionResult> GetIssueByIssueId(int issueId)
{
    var issue = await _repo.GetIssueByIssueId(issueId);

    if (issue == null)
        return NotFound("Issue not found");

    return Ok(issue);
}

[Authorize]
[HttpGet("user/search/{userId}")]
public async Task<IActionResult> SearchByUserId(int userId)
{
    var issues = await _repo.GetIssuesByUserId(userId);

    if (issues.Count == 0)
        return NotFound("No issued books found for this user");

    return Ok(issues);
}


    }
}
