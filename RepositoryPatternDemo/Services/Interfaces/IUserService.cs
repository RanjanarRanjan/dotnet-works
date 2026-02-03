using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
    }
}
