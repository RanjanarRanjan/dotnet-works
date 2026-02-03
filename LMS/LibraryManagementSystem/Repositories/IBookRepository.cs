// using LibraryManagementSystem.Models;

// namespace LibraryManagementSystem.Repositories
// {
//     public interface IBookRepository
//     {
//         Task<Book?> AddBook(Book book);
//         Task<Book?> UpdateBook(Book book);
//         Task<bool> DeleteBook(int id);

//         Task<Book?> GetBookById(int id);
//         Task<Book?> GetBookByIdSimple(int bookId);
//         Task<List<Book>> SearchBooks(int? bookId, string? bookName, string? author);


//         Task<(List<Book>, int)> GetAllBooksPaged(int pageNumber, int pageSize);
//         Task<(List<Book>, int)> GetActiveBooksPaged(int pageNumber, int pageSize);

//         Task<bool> BookExists(string bookName, string author);


        
//     }
// }
using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> AddBookWithCategories(AddBookDto dto);
        Task<Book?> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);

        Task<Book?> GetBookById(int id);
        Task<List<Book>> SearchBooks(int? bookId, string? bookName, string? author);
        Task<List<Book>> SearchByCategory(string category);

        Task<(List<Book>, int)> GetAllBooksPaged(int pageNumber, int pageSize);
        Task<(List<Book>, int)> GetActiveBooksPaged(int pageNumber, int pageSize);

        Task<bool> BookExists(string bookName, string author);
    }
}
