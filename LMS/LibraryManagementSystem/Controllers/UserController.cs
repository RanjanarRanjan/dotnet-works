// using LibraryManagementSystem.Models;
// using LibraryManagementSystem.Repositories;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace LibraryManagementSystem.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserController : ControllerBase
//     {
//         private readonly IUserRepository _repo;
//         public UserController(IUserRepository repo) => _repo = repo;

//         [Authorize(Roles = "Admin")]
//         [HttpPost("create")]
//         public async Task<IActionResult> CreateUser([FromBody] User user)
//         {
//             var created = await _repo.CreateUser(user);
//             return Ok(created);
//         }

//         // [Authorize(Roles = "Admin")]
//         // [HttpGet("all")]
//         // public async Task<IActionResult> GetAllUsers()
//         // {
//         //     var users = await _repo.GetAllUsers();
//         //     return Ok(users);
//         // }

//         [Authorize]
//         [HttpGet("me/{id}")]
//         public async Task<IActionResult> GetMyProfile(int id)
//         {
//             var user = await _repo.GetUserById(id);
//             if (user == null) return NotFound();
//             return Ok(user);
//         }

//         // [Authorize(Roles = "Admin")]
//         // [HttpPatch("update/admin/{id}")]
//         // public async Task<IActionResult> UpdateUserByAdmin(int id, [FromBody] User update)
//         // {
//         //     var user = await _repo.GetUserById(id);
//         //     if (user == null) return NotFound();
//         //     user.ContactNumber = update.ContactNumber;
//         //     user.Department = update.Department;
//         //     var updated = await _repo.UpdateUser(user);
//         //     return Ok(updated);
//         // }
// [Authorize(Roles = "Admin")]
// [HttpPatch("update/admin/{id}")]
// public async Task<IActionResult> AdminUpdateUser(
//     int id,
//     [FromBody] AdminUpdateUserDto dto)
// {
//     var user = await _repo.GetUserById(id);
//     if (user == null || user.IsDeleted)
//         return NotFound();

//     user.ContactNumber = dto.ContactNumber;
//     user.Department = dto.Department;

//     await _repo.UpdateUser(user);
//     return Ok("User updated by admin");
// }





//         [Authorize]
//         [HttpPatch("update/user/{id}")]
//         public async Task<IActionResult> UpdateUserByUser(int id, [FromBody] User update)
//         {
//             var user = await _repo.GetUserById(id);
//             if (user == null) return NotFound();
//             user.ContactNumber = update.ContactNumber;
//             user.Department = update.Department;
//             user.Password = update.Password;
//             var updated = await _repo.UpdateUser(user);
//             return Ok(updated);
//         }

//         [Authorize(Roles = "Admin")]
//         [HttpPatch("block/{id}")]
//         public async Task<IActionResult> BlockUser(int id)
//         {
//             var user = await _repo.BlockUser(id);
//             if (user == null) return NotFound();
//             return Ok(user);
//         }

//         [Authorize(Roles = "Admin")]
//         [HttpGet("all")]
//         public async Task<IActionResult> GetAllUsers(
//             int pageNumber = 1,
//             int pageSize = 5)
//         {
//             var (users, totalCount) = await _repo.GetUsersPaged(pageNumber, pageSize);
//             return Ok(new
//             {
//                 PageNumber = pageNumber,
//                 PageSize = pageSize,
//                 TotalUsers = totalCount,
//                 TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
//                 Data = users
//             });
//         }

//         [Authorize(Roles = "Admin")]
//         [HttpGet("active")]
//         public async Task<IActionResult> GetActiveUsers(
//         [FromQuery] int pageNumber = 1,
//         [FromQuery] int pageSize = 5)
//         {
//             var (users, totalCount) = await _repo.GetActiveUsersPaged(pageNumber, pageSize);

//             return Ok(new
//             {
//                 PageNumber = pageNumber,
//                 PageSize = pageSize,
//                 TotalUsers = totalCount,
//                 TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
//                 Data = users
//             });
//         }


//     }
// }
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        // ============================
        // ADMIN: CREATE USER
        // ============================
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var created = await _repo.CreateUser(user);
            return Ok(created);
        }

        // ============================
        // USER: GET OWN PROFILE
        // ============================
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _repo.GetUserByUsername(username);

            if (user == null || user.IsDeleted || user.IsBlocked)
                return Unauthorized();

            return Ok(user);
        }

        // ============================
        // ADMIN: UPDATE USER (NO PASSWORD / USERNAME)
        // ============================
        [Authorize(Roles = "Admin")]
        [HttpPatch("update/admin/{id}")]
        public async Task<IActionResult> AdminUpdateUser(
            int id,
            [FromBody] AdminUpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _repo.GetUserById(id);

            if (user == null || user.IsDeleted)
                return NotFound();

            user.ContactNumber = dto.ContactNumber;
            user.Department = dto.Department;

            await _repo.UpdateUser(user);
            return Ok("User updated by admin");
        }

        // ============================
        // USER: UPDATE OWN PROFILE
        // ============================
        [Authorize]
        [HttpPatch("update/profile")]
        public async Task<IActionResult> UpdateOwnProfile(
            [FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _repo.GetUserByUsername(username);

            if (user == null || user.IsDeleted || user.IsBlocked)
                return Unauthorized();

            user.ContactNumber = dto.ContactNumber;
            user.Department = dto.Department;
            user.Password = dto.Password;

            await _repo.UpdateUser(user);
            return Ok("Profile updated successfully");
        }

        // ============================
        // ADMIN: BLOCK USER
        // ============================
        [Authorize(Roles = "Admin")]
        [HttpPatch("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var user = await _repo.BlockUser(id);

            if (user == null)
                return NotFound();

            return Ok("User blocked successfully");
        }

        // ============================
        // ADMIN: GET ALL USERS (PAGINATED)
        // ============================
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            var (users, totalCount) =
                await _repo.GetUsersPaged(pageNumber, pageSize);

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalUsers = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = users
            });
        }

        // ============================
        // ADMIN: GET ACTIVE USERS ONLY
        // ============================
        [Authorize(Roles = "Admin")]
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            var (users, totalCount) =
                await _repo.GetActiveUsersPaged(pageNumber, pageSize);

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalUsers = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = users
            });
        }

        [Authorize(Roles = "Admin")]
[HttpPatch("unblock/{id}")]
public async Task<IActionResult> UnblockUser(int id)
{
    var user = await _repo.UnblockUser(id);

    if (user == null)
        return NotFound("User not found or deleted");

    return Ok("User unblocked successfully");
}



[Authorize(Roles = "Admin")]
[HttpGet("blocked")]
public async Task<IActionResult> GetBlockedUsers(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 5)
{
    var (users, totalCount) =
        await _repo.GetBlockedUsersPaged(pageNumber, pageSize);

    return Ok(new
    {
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalUsers = totalCount,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        Data = users
    });
}


    }
}
