using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Users table
        public DbSet<User> Users { get; set; } = null!;

        // Books table
        // public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Book> Books { get; set; }
public DbSet<Category> Categories { get; set; }
public DbSet<BookCategory> BookCategories { get; set; }


        // BookIssues table
        public DbSet<BookIssue> BookIssues { get; set; } = null!;

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     // Unique index on Username
        //     modelBuilder.Entity<User>()
        //         .HasIndex(u => u.Username)
        //         .IsUnique();

        //     // Optional: Unique index for bookName + author combination
        //     modelBuilder.Entity<Book>()
        //         .HasIndex(b => new { b.BookName, b.Author })
        //         .IsUnique();

            
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<BookCategory>()
        .HasKey(bc => new { bc.BookID, bc.CategoryId });

    modelBuilder.Entity<BookCategory>()
        .HasOne(bc => bc.Book)
        .WithMany(b => b.BookCategories)
        .HasForeignKey(bc => bc.BookID);

    modelBuilder.Entity<BookCategory>()
        .HasOne(bc => bc.Category)
        .WithMany(c => c.BookCategories)
        .HasForeignKey(bc => bc.CategoryId);
}

    }
}
