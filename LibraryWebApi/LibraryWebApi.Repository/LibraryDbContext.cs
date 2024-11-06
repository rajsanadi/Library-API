using LibraryWebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Repository
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.BookID);
                entity.Property(b => b.Title).IsRequired();
                entity.Property(b => b.ISBN).IsRequired();

                // Configure one-to-many relationship with BorrowedBook
                entity.HasMany(b => b.BorrowedBooks)
                      .WithOne(bb => bb.Book)
                      .HasForeignKey(bb => bb.BookID);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserID);
                entity.Property(u => u.Username).IsRequired();

                // Configure one-to-many relationship with BorrowedBook
                entity.HasMany(u => u.BorrowedBooks)
                      .WithOne(bb => bb.User)
                      .HasForeignKey(bb => bb.UserID);
            });

            // Configure BorrowedBook entity
            modelBuilder.Entity<BorrowedBook>(entity =>
            {
                entity.HasKey(bb => bb.BorrowedID);
                entity.Property(bb => bb.BorrowDate).IsRequired();
                entity.HasOne(bb => bb.Book)
                      .WithMany(b => b.BorrowedBooks)
                      .HasForeignKey(bb => bb.BookID);
                entity.HasOne(bb => bb.User)
                      .WithMany(u => u.BorrowedBooks)
                      .HasForeignKey(bb => bb.UserID);
            });
        }
    }
}
