using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<(List<User>, int)> GetUsersPaged(int pageNumber, int pageSize);
        Task<(List<User>, int)> GetActiveUsersPaged(int pageNumber, int pageSize);

        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsername(string username);

        Task<User?> CreateUser(User user);
        Task<User?> UpdateUser(User user);
        Task<bool> DeleteUser(int id); // Soft delete

        Task<User?> BlockUser(int id);
        Task<User?> UnblockUser(int id);
        Task<(List<User>, int)> GetBlockedUsersPaged(int pageNumber, int pageSize);


    }
}
