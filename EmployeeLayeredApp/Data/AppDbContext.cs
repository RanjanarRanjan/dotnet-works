// using EmployeeLayeredApp.Models;
// using Microsoft.EntityFrameworkCore;

// namespace EmployeeLayeredApp.Data
// {
//     public class AppDbContext : DbContext
//     {
//         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//         public DbSet<Employee> Employees { get; set; }
//     }
// }
// using EmployeeLayeredApp.Models;
// using Microsoft.EntityFrameworkCore;

// namespace EmployeeLayeredApp.Data
// {
//     public class AppDbContext : DbContext
//     {
//         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//         public DbSet<Employee> Employees { get; set; }
//         public DbSet<User> Users { get; set; }
//     }
// }




using EmployeeLayeredApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLayeredApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Unique email only for active employees
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            //  Username unique & case-sensitive
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}









// using EmployeeLayeredApp.Models;
// using Microsoft.EntityFrameworkCore;

// namespace EmployeeLayeredApp.Data
// {
//     public class AppDbContext : DbContext
//     {
//         public AppDbContext(DbContextOptions<AppDbContext> options)
//             : base(options)
//         {
//         }

//         public DbSet<Employee> Employees { get; set; }
//         public DbSet<User> Users { get; set; }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             //  Employee Email unique only for active (IsDeleted = 0)
//             modelBuilder.Entity<Employee>()
//                 .HasIndex(e => e.Email)
//                 .IsUnique()
//                 .HasFilter("[IsDeleted] = 0");

//             //  USERNAME: UNIQUE + CASE-SENSITIVE
//             modelBuilder.Entity<User>()
//                 .Property(u => u.Username)
//                 .UseCollation("SQL_Latin1_General_CP1_CS_AS");

//             modelBuilder.Entity<User>()
//                 .HasIndex(u => u.Username)
//                 .IsUnique();
//         }
//     }
// }
