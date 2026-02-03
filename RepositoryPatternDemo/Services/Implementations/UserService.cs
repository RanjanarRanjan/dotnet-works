using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public List<User> GetUsers()
        {
            return _repository.GetAllUsers();
        }
    }
}
