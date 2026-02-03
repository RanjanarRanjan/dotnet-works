// using EmployeeLayeredApp.Data;
// using EmployeeLayeredApp.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using System.Linq;

// namespace EmployeeLayeredApp.Repositories
// {
//     public class EmployeeRepository : IEmployeeRepository
//     {
//         private readonly AppDbContext _context;

//         public EmployeeRepository(AppDbContext context)
//         {
//             _context = context;
//         }

//         public List<Employee> GetAll()
//         {
//             return _context.Employees.ToList();
//         }

//         public Employee? GetById(int id)
//         {
//             return _context.Employees.FirstOrDefault(e => e.Id == id);
//         }

//         public void Add(Employee emp)
//         {
//             _context.Employees.Add(emp);
//             _context.SaveChanges();
//         }

//         public void Update(Employee emp)
//         {
//             _context.Employees.Update(emp);
//             _context.SaveChanges();
//         }

//         public void Delete(int id)
//         {
//             var emp = _context.Employees.Find(id);
//             if (emp != null)
//             {
//                 _context.Employees.Remove(emp);
//                 _context.SaveChanges();
//             }
//         }
//     }
// }




using EmployeeLayeredApp.Data;
using EmployeeLayeredApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeLayeredApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll(int pageNumber, int pageSize)
        {
            return _context.Employees
                .Where(e => !e.IsDeleted)
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalCount()
        {
            return _context.Employees.Count(e => !e.IsDeleted);
        }

        public Employee? GetById(int id)
        {
            return _context.Employees
                .FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        }

        public void Add(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
        }

        public void Update(Employee emp)
        {
            _context.Employees.Update(emp);
            _context.SaveChanges();
        }

        public void SoftDelete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                emp.IsDeleted = true;
                _context.SaveChanges();
            }
        }

        public bool EmailExists(string email)
        {
            return _context.Employees
                .Any(e => e.Email == email && !e.IsDeleted);
        }

        public bool EmailExistsForUpdate(int id, string email)
        {
            return _context.Employees
                .Any(e => e.Email == email && e.Id != id && !e.IsDeleted);
        }
    }
}




// using EmployeeLayeredApp.Data;
// using EmployeeLayeredApp.Models;
// using System.Collections.Generic;
// using System.Linq;

// namespace EmployeeLayeredApp.Repositories
// {
//     public class EmployeeRepository : IEmployeeRepository
//     {
//         private readonly AppDbContext _context;

//         public EmployeeRepository(AppDbContext context)
//         {
//             _context = context;
//         }

//         // Pagination + only non-deleted employees
//         public List<Employee> GetAll(int pageNumber, int pageSize)
//         {
//             return _context.Employees
//                 .Where(e => !e.IsDeleted)
//                 .Skip((pageNumber - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToList();
//         }

//         public int GetTotalCount()
//         {
//             return _context.Employees.Count(e => !e.IsDeleted);
//         }

//         public Employee? GetById(int id)
//         {
//             return _context.Employees
//                 .FirstOrDefault(e => e.Id == id && !e.IsDeleted);
//         }

//         public void Add(Employee emp)
//         {
//             _context.Employees.Add(emp);
//             _context.SaveChanges();
//         }


//         public bool EmailExists(string email)
//         {
//             return _context.Employees.Any(e => e.Email == email);
//         }

//         public bool EmailExistsForUpdate(int id, string email)
//         {
//             return _context.Employees.Any(e => e.Email == email && e.Id != id);
//         }



//         public void Update(Employee emp)
//         {
//             _context.Employees.Update(emp);
//             _context.SaveChanges();
//         }

//         // SOFT DELETE
//         public void SoftDelete(int id)
//         {
//             var emp = _context.Employees.Find(id);
//             if (emp != null)
//             {
//                 emp.IsDeleted = true;
//                 _context.SaveChanges();
//             }
//         }
//     }
// }
