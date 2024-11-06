using LibraryWebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApi.Repository
{
    public interface IBorrowedBookRepository
    {
        Task<IEnumerable<BorrowedBook>> GetAllAsync();
        Task<BorrowedBook> GetByIdAsync(int id);
        Task AddAsync(BorrowedBook borrowedBook);
        Task UpdateAsync(BorrowedBook borrowedBook);
        Task DeleteAsync(int id);

        // New methods
        Task<IEnumerable<BorrowedBook>> GetByBookTitleAsync(string title);
        Task<IEnumerable<BorrowedBook>> GetByUserIdAsync(int userId);
        Task<IEnumerable<BorrowedBook>> GetByUserIdWithReturnDateAsync(int userId);

        Task<IEnumerable<User>> GetUsersByBookIdAsync(int bookId); 
        Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId); 
    }

    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowedBookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowedBook>> GetAllAsync()
        {
            return await _context.BorrowedBooks.ToListAsync();
        }

        public async Task<BorrowedBook> GetByIdAsync(int id)
        {
            return await _context.BorrowedBooks.FindAsync(id);
        }

        public async Task AddAsync(BorrowedBook borrowedBook)
        {
            _context.BorrowedBooks.Add(borrowedBook);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowedBook borrowedBook)
        {
            _context.BorrowedBooks.Update(borrowedBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook != null)
            {
                _context.BorrowedBooks.Remove(borrowedBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BorrowedBook>> GetByBookTitleAsync(string title)
        {
            return await _context.BorrowedBooks
                .Include(b => b.Book) // Include the Book entity in the query
                .Where(b => EF.Functions.Like(b.Book.Title, $"%{title}%")) // Case-insensitive search
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowedBook>> GetByUserIdAsync(int userId)
        {
            return await _context.BorrowedBooks
                .Where(bb => bb.UserID == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowedBook>> GetByUserIdWithReturnDateAsync(int userId)
        {
            return await _context.BorrowedBooks
                .Where(bb => bb.UserID == userId && bb.ReturnDate.HasValue)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByBookIdAsync(int bookId)
        {
            return await _context.BorrowedBooks
                .Where(b => b.BookID == bookId)
                .Select(b => b.User)
                .Distinct()
                .ToListAsync();
        }

       
        public async Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId)
        {
            return await _context.BorrowedBooks
                .Where(b => b.UserID == userId)
                .Select(b => b.Book)
                .Distinct()
                .ToListAsync();
        }
    }
}
