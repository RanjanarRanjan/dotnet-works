using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserByUsername(string username);
    }
}
