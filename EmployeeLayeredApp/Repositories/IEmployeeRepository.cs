// using EmployeeLayeredApp.Models;
// using System.Collections.Generic;

// namespace EmployeeLayeredApp.Repositories
// {
//     public interface IEmployeeRepository
//     {
//         List<Employee> GetAll();
//          Employee? GetById(int id);
//         void Add(Employee emp);
//         void Update(Employee emp);
//         void Delete(int id);
//     }
// }
using EmployeeLayeredApp.Models;
using System.Collections.Generic;

namespace EmployeeLayeredApp.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll(int pageNumber, int pageSize);
        int GetTotalCount();
        Employee? GetById(int id);
        void Add(Employee emp);
        void Update(Employee emp);
        void SoftDelete(int id);

        bool EmailExists(string email);
        bool EmailExistsForUpdate(int id, string email);
    }
}
