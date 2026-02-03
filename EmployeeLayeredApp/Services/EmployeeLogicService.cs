// using EmployeeLayeredApp.Models;
// using EmployeeLayeredApp.Repositories;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace EmployeeLayeredApp.Services
// {
//     public class EmployeeLogicService
//     {
//         private readonly IEmployeeRepository _repo;

//         public EmployeeLogicService(IEmployeeRepository repo)
//         {
//             _repo = repo;
//         }

//         public List<Employee> GetActiveEmployees()
//         {
//             // For demo, all employees are considered active
//             return _repo.GetAll();
//         }

//         public Employee GetEmployee(int id)
//         {
//             return _repo.GetById(id);
//         }

//         public void AddEmployee(Employee emp)
//         {
//             if (string.IsNullOrEmpty(emp.Name) || string.IsNullOrEmpty(emp.Email))
//                 throw new Exception("Name and Email are required");

//             _repo.Add(emp);
//         }

//         public void UpdateEmployee(Employee emp)
//         {
//             _repo.Update(emp);
//         }

//         public void DeleteEmployee(int id)
//         {
//             _repo.Delete(id);
//         }
//     }
// }





using EmployeeLayeredApp.Models;
using EmployeeLayeredApp.Repositories;
using System;
using System.Collections.Generic;

namespace EmployeeLayeredApp.Services
{
    public class EmployeeLogicService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeLogicService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public List<Employee> GetEmployees(int pageNumber, int pageSize)
        {
            return _repo.GetAll(pageNumber, pageSize);
        }

        public int GetTotalEmployees()
        {
            return _repo.GetTotalCount();
        }

        public Employee GetEmployee(int id)
        {
            return _repo.GetById(id)
                   ?? throw new Exception("Employee not found");
        }

        public void AddEmployee(Employee emp)
        {
            if (_repo.EmailExists(emp.Email))
                throw new Exception("Employee email already exists");

            _repo.Add(emp);
        }

        public void UpdateEmployee(Employee emp)
        {
            if (_repo.EmailExistsForUpdate(emp.Id, emp.Email))
                throw new Exception("Employee email already exists");

            _repo.Update(emp);
        }

        public void DeleteEmployee(int id)
        {
            _repo.SoftDelete(id);
        }
    }
}





// using EmployeeLayeredApp.Models;
// using EmployeeLayeredApp.Repositories;
// using System;
// using System.Collections.Generic;

// namespace EmployeeLayeredApp.Services
// {
//     public class EmployeeLogicService
//     {
//         private readonly IEmployeeRepository _repo;

//         public EmployeeLogicService(IEmployeeRepository repo)
//         {
//             _repo = repo;
//         }

//         public List<Employee> GetEmployees(int pageNumber, int pageSize)
//         {
//             return _repo.GetAll(pageNumber, pageSize);
//         }

//         public int GetTotalEmployees()
//         {
//             return _repo.GetTotalCount();
//         }

//         public Employee GetEmployee(int id)
//         {
//             return _repo.GetById(id);
//         }

//         public void AddEmployee(Employee emp)
//         {
//             if (string.IsNullOrEmpty(emp.Name) || string.IsNullOrEmpty(emp.Email))
//                 throw new Exception("Name and Email are required");
//             if (_repo.EmailExists(emp.Email))
//                 throw new Exception("Employee email already exists");


//             _repo.Add(emp);
//         }

//         public void UpdateEmployee(Employee emp)
//         {
//             if (_repo.EmailExistsForUpdate(emp.Id, emp.Email))
//                 throw new Exception("Employee email already exists");

//             _repo.Update(emp);
//         }

//         public void DeleteEmployee(int id)
//         {
//             _repo.SoftDelete(id);
//         }
//     }
// }
