using EmployeeLayeredApp.Data;
using EmployeeLayeredApp.Models;
using System.Linq;

namespace EmployeeLayeredApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Register(User user)
        {
            var existingUser = GetByUsername(user.Username);
            if (existingUser != null)
                throw new Exception("Username already exists");

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
