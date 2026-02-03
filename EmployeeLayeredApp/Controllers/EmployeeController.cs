// using EmployeeLayeredApp.Models;
// using EmployeeLayeredApp.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace EmployeeLayeredApp.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class EmployeeController : ControllerBase
//     {
//         private readonly EmployeeWrapperService _wrapper;

//         public EmployeeController(EmployeeWrapperService wrapper)
//         {
//             _wrapper = wrapper;
//         }

//         [HttpGet]
//         public IActionResult GetAll()
//         {
//             return Ok(_wrapper.GetEmployees());
//         }

//         [HttpGet("{id}")]
//         public IActionResult GetById(int id)
//         {
//             var emp = _wrapper.GetEmployeeById(id);
//             if (emp == null) return NotFound();
//             return Ok(emp);
//         }

//         [HttpPost]
//         public IActionResult Create([FromBody] Employee emp)
//         {
//             _wrapper.CreateEmployee(emp);
//             return Ok(emp);
//         }

//         [HttpPut("{id}")]
//         public IActionResult Update(int id, [FromBody] Employee emp)
//         {
//             emp.Id = id;
//             _wrapper.UpdateEmployee(emp);
//             return Ok(emp);
//         }

//         [HttpDelete("{id}")]
//         public IActionResult Delete(int id)
//         {
//             _wrapper.DeleteEmployee(id);
//             return Ok();
//         }
//     }
// }
using EmployeeLayeredApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLayeredApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeWrapperService _wrapper;

        public EmployeeController(EmployeeWrapperService wrapper)
        {
            _wrapper = wrapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 5)
        {
            return Ok(_wrapper.GetEmployees(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Models.Employee emp)
        {
            try
            {
                _wrapper.CreateEmployee(emp);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Employee emp)
        {
            try
            {    
                emp.Id = id;
                _wrapper.UpdateEmployee(emp);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _wrapper.DeleteEmployee(id);
            return Ok("Employee deleted");
        }
    }
}


// using EmployeeLayeredApp.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace EmployeeLayeredApp.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class EmployeeController : ControllerBase
//     {
//         private readonly EmployeeWrapperService _wrapper;

//         public EmployeeController(EmployeeWrapperService wrapper)
//         {
//             _wrapper = wrapper;
//         }

//         // Pagination
//         [HttpGet]
//         public IActionResult GetAll(
//             int pageNumber = 1,
//             int pageSize = 5)
//         {
//             var employees = _wrapper.GetEmployees(pageNumber, pageSize);
//             var totalCount = _wrapper.GetTotalEmployees();

//             return Ok(new
//             {
//                 PageNumber = pageNumber,
//                 PageSize = pageSize,
//                 TotalRecords = totalCount,
//                 Data = employees
//             });
//         }

//         [HttpGet("{id}")]
//         public IActionResult GetById(int id)
//         {
//             var emp = _wrapper.GetEmployeeById(id);
//             if (emp == null) return NotFound();
//             return Ok(emp);
//         }

//         [HttpPost]
//         public IActionResult Create([FromBody] Models.Employee emp)
//         {
//             _wrapper.CreateEmployee(emp);
//             return Ok(emp);
//         }

//         [HttpPut("{id}")]
//         public IActionResult Update(int id, [FromBody] Models.Employee emp)
//         {
//             emp.Id = id;
//             _wrapper.UpdateEmployee(emp);
//             return Ok(emp);
//         }

//         // SOFT DELETE
//         [HttpDelete("{id}")]
//         public IActionResult Delete(int id)
//         {
//             _wrapper.DeleteEmployee(id);
//             return Ok(new { message = "Employee soft deleted (IsDeleted = 1)" });
//         }
//     }
// }

