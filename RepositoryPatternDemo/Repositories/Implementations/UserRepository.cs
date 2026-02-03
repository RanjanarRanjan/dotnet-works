using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users
                           .FirstOrDefault(u => u.Username == username);
        }
    }
}
