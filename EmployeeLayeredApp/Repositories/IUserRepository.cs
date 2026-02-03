using EmployeeLayeredApp.Models;

namespace EmployeeLayeredApp.Repositories
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        void Register(User user);
    }
}
