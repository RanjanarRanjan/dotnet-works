// using LibraryManagementSystem.Models;

// namespace LibraryManagementSystem.Repositories
// {
//     public interface IBookIssueRepository
//     {
//         Task<BookIssue?> IssueBook(int bookId, int userId);
//         Task<BookIssue?> MarkAsReturned(int issueId);

//         Task<(List<BookIssue>, int)> GetIssuedBooksByBook(
//             string search, int pageNumber, int pageSize);

//         Task<(List<BookIssue>, int)> GetBorrowedBooksByUser(
//             int userId, int pageNumber, int pageSize);
//     }
// }
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookIssueRepository
    {
        // Issue / Return
        Task<BookIssue?> IssueBook(int bookId, int userId);
        Task<BookIssue?> MarkAsReturned(int issueId);

        // Admin Queries
        Task<List<BookIssue>> GetAllIssuedBooks();
        Task<List<BookIssue>> GetReturnedBooks();
        Task<List<BookIssue>> GetNotReturnedBooks();
        Task<List<BookIssue>> GetIssuedBooksByBookId(int bookId);
        Task<BookIssue?> GetIssueByIssueId(int issueId);
        Task<List<BookIssue>> GetIssuesByUserId(int userId);



        // User Queries
        Task<(List<BookIssue>, int)> GetBorrowedBooksByUser(
            int userId, int pageNumber, int pageSize);
    }
}
