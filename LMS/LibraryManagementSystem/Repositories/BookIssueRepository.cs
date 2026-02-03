using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class BookIssueRepository : IBookIssueRepository
    {
        private readonly AppDbContext _context;

        public BookIssueRepository(AppDbContext context)
        {
            _context = context;
        }

        // =====================
        // ISSUE BOOK
        // =====================
        public async Task<BookIssue?> IssueBook(int bookId, int userId)
        {
            //Check if book exists and has available copies
            var book = await _context.Books
                .FirstOrDefaultAsync(b =>
                    b.BookID == bookId &&
                    !b.IsDeleted &&
                    b.AvailableCopies > 0);

            if (book == null)
                return null; // book not found or no copies available

            // Check if user exists and not blocked
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.UserID == userId &&
                    !u.IsDeleted &&
                    !u.IsBlocked);

            if (user == null)
                return null; // invalid user

            // Reduce available copies
            book.AvailableCopies--;

            //  Create issue record
            var issue = new BookIssue
            {
                BookId = book.BookID,
                BookName = book.BookName,
                UserId = user.UserID,
                IssueDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(20),
                ReturnStatus = "Not Returned"
            };

            _context.BookIssues.Add(issue);
            await _context.SaveChangesAsync();

            return issue;
        }

        // =====================
        // RETURN BOOK
        // =====================
        public async Task<BookIssue?> MarkAsReturned(int issueId)
        {
            var issue = await _context.BookIssues
                .FirstOrDefaultAsync(i =>
                    i.IssueId == issueId &&
                    i.ReturnStatus == "Not Returned");

            if (issue == null)
                return null; // issue not found or already returned

            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.BookID == issue.BookId);

            if (book == null)
                return null; // book record missing

            // Mark as returned
            issue.ReturnStatus = "Returned";

            // Increase available copies
            book.AvailableCopies++;

            await _context.SaveChangesAsync();
            return issue;
        }

        // =====================
        // GET ALL ISSUED BOOKS
        // =====================
        public async Task<List<BookIssue>> GetAllIssuedBooks()
        {
            return await _context.BookIssues
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();
        }

        // =====================
        // GET RETURNED BOOKS
        // =====================
        public async Task<List<BookIssue>> GetReturnedBooks()
        {
            return await _context.BookIssues
                .Where(i => i.ReturnStatus == "Returned")
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();
        }

        // =====================
        // GET NOT RETURNED BOOKS
        // =====================
        public async Task<List<BookIssue>> GetNotReturnedBooks()
        {
            return await _context.BookIssues
                .Where(i => i.ReturnStatus == "Not Returned")
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();
        }

        // =====================
        // GET ISSUED BOOKS BY BOOK ID
        // =====================
        public async Task<List<BookIssue>> GetIssuedBooksByBookId(int bookId)
        {
            return await _context.BookIssues
                .Where(i => i.BookId == bookId)
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();
        }

        // =====================
        // SEARCH ISSUED BOOKS BY USER ID
        // =====================
        public async Task<List<BookIssue>> GetIssuesByUserId(int userId)
        {
            return await _context.BookIssues
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();
        }

        // =====================
        // GET ISSUE DETAILS BY ISSUE ID
        // =====================
        public async Task<BookIssue?> GetIssueByIssueId(int issueId)
        {
            return await _context.BookIssues
                .FirstOrDefaultAsync(i => i.IssueId == issueId);
        }

        // =====================
        // GET BORROWED BOOKS BY USER (PAGINATED)
        // =====================
        public async Task<(List<BookIssue>, int)> GetBorrowedBooksByUser(
            int userId, int pageNumber, int pageSize)
        {
            var query = _context.BookIssues
                .Where(i =>
                    i.UserId == userId &&
                    i.ReturnStatus == "Not Returned");

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }
    }
}

