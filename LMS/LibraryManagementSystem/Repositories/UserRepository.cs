// using LibraryManagementSystem.Data;
// using LibraryManagementSystem.Models;
// using Microsoft.EntityFrameworkCore;

// namespace LibraryManagementSystem.Repositories
// {
//     public class UserRepository : IUserRepository
//     {
//         private readonly AppDbContext _context;
//         public UserRepository(AppDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<User?> CreateUser(User user)
//         {
//             _context.Users.Add(user);
//             await _context.SaveChangesAsync();
//             return user;
//         }

//         public async Task<bool> DeleteUser(int id)
//         {
//             var user = await _context.Users.FindAsync(id);
//             if (user == null) return false;
//             user.IsDeleted = true;
//             await _context.SaveChangesAsync();
//             return true;
//         }

//         public async Task<List<User>> GetAllUsers()
//         {
//             return await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
//         }

//         public async Task<(List<User>, int)> GetUsersPaged(int pageNumber, int pageSize)
//         {
//             var query = _context.Users
//                 .Where(u => !u.IsDeleted);

//             int totalCount = await query.CountAsync();

//             var users = await query
//                 .OrderBy(u => u.UserID)
//                 .Skip((pageNumber - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToListAsync();

//             return (users, totalCount);
//         }



//         public async Task<User?> GetUserById(int id)
//         {
//             return await _context.Users.FirstOrDefaultAsync(u => u.UserID == id && !u.IsDeleted);
//         }

//         public async Task<User?> UpdateUser(User user)
//         {
//             _context.Users.Update(user);
//             await _context.SaveChangesAsync();
//             return user;
//         }

//         public async Task<User?> BlockUser(int id)
//         {
//             var user = await _context.Users.FindAsync(id);
//             if (user == null) return null;
//             user.IsBlocked = true;
//             await _context.SaveChangesAsync();
//             return user;
//         }

//         public async Task<(List<User>, int)> GetActiveUsersPaged(int pageNumber, int pageSize)
//         {
//             var query = _context.Users
//                 .Where(u => !u.IsDeleted && !u.IsBlocked);

//             int totalCount = await query.CountAsync();

//             var users = await query
//                 .OrderBy(u => u.UserID)
//                 .Skip((pageNumber - 1) * pageSize)
//                 .Take(pageSize)
//                 .ToListAsync();

//             return (users, totalCount);
//         }
//     }
// }

using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // ============================
        // CREATE USER
        // ============================
        public async Task<User?> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // ============================
        // GET USER BY ID
        // ============================
        public async Task<User?> GetUserById(int id)
{
    return await _context.Users
        .FirstOrDefaultAsync(u => u.UserID == id && !u.IsDeleted);
}

        // public async Task<User?> GetUserById(int id)
        // {
        //     return await _context.Users
        //         .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        // }

        // ============================
        // GET USER BY USERNAME
        // ============================
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted);
        }

        // ============================
        // UPDATE USER
        // ============================
        public async Task<User?> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // ============================
        // SOFT DELETE USER
        // ============================
       public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user == null) return false;

                user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }


        // ============================
        // BLOCK USER
        // ============================
        public async Task<User?> BlockUser(int id)
        {
            var user = await GetUserById(id);
            if (user == null) return null;

                user.IsBlocked = true;
            await _context.SaveChangesAsync();
            return user;
        }


        // ============================
        // GET ALL USERS (NO PAGINATION)
        // ============================
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        // ============================
        // GET USERS WITH PAGINATION
        // ============================
        public async Task<(List<User>, int)> GetUsersPaged(int pageNumber, int pageSize)
        {
            var query = _context.Users.Where(u => !u.IsDeleted);

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        // ============================
        // GET ACTIVE USERS (NOT DELETED & NOT BLOCKED)
        // ============================
        public async Task<(List<User>, int)> GetActiveUsersPaged(int pageNumber, int pageSize)
        {
            var query = _context.Users
                .Where(u => !u.IsDeleted && !u.IsBlocked);

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        // ============================
        // PATCH Unblock USERS 
        // ============================
        public async Task<User?> UnblockUser(int id)
        {
            var user = await GetUserById(id);

            if (user == null || user.IsDeleted)
                return null;

                user.IsBlocked = false;
                await _context.SaveChangesAsync();

            return user;
        }

        // ============================
        // GET Unblock USERS 
        // ============================

        public async Task<(List<User>, int)> GetBlockedUsersPaged(int pageNumber, int pageSize)
        {
            var query = _context.Users
                .Where(u => !u.IsDeleted && u.IsBlocked);

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }


    }
}
