using EmployeeLayeredApp.Models;
using EmployeeLayeredApp.Repositories;

namespace EmployeeLayeredApp.Services
{
    public class AuthService
    {
        private readonly IUserRepository _repo;
        private readonly JwtService _jwt;

        public AuthService(IUserRepository repo, JwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public string Login(string username, string password)
        {
            var user = _repo.GetByUsername(username);

            if (user == null || user.Password != password)
                throw new Exception("Invalid username or password");

            // ROLE IS TAKEN FROM DATABASE
            return _jwt.GenerateToken(user.Username, user.Role);
        }

        public void Register(User user)
        {
            _repo.Register(user);
        }
    }
}
