// using EmployeeLayeredApp.Models;
// using System.Collections.Generic;

// namespace EmployeeLayeredApp.Services
// {
//     public class EmployeeWrapperService
//     {
//         private readonly EmployeeLogicService _logic;

//         public EmployeeWrapperService(EmployeeLogicService logic)
//         {
//             _logic = logic;
//         }

//         public List<Employee> GetEmployees()
//         {
//             return _logic.GetActiveEmployees();
//         }

//         public Employee GetEmployeeById(int id)
//         {
//             return _logic.GetEmployee(id);
//         }

//         public void CreateEmployee(Employee emp)
//         {
//             _logic.AddEmployee(emp);
//         }

//         public void UpdateEmployee(Employee emp)
//         {
//             _logic.UpdateEmployee(emp);
//         }

//         public void DeleteEmployee(int id)
//         {
//             _logic.DeleteEmployee(id);
//         }
//     }
// }


using EmployeeLayeredApp.Models;
using System.Collections.Generic;

namespace EmployeeLayeredApp.Services
{
    public class EmployeeWrapperService
    {
        private readonly EmployeeLogicService _logic;

        public EmployeeWrapperService(EmployeeLogicService logic)
        {
            _logic = logic;
        }

        public List<Employee> GetEmployees(int pageNumber, int pageSize)
        {
            return _logic.GetEmployees(pageNumber, pageSize);
        }

        public int GetTotalEmployees()
        {
            return _logic.GetTotalEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            return _logic.GetEmployee(id);
        }

        public void CreateEmployee(Employee emp)
        {
            _logic.AddEmployee(emp);
        }

        public void UpdateEmployee(Employee emp)
        {
            _logic.UpdateEmployee(emp);
        }

        public void DeleteEmployee(int id)
        {
            _logic.DeleteEmployee(id);
        }
    }
}

