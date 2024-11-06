using LibraryWebApi.Domain.Models;
using LibraryWebApi.Repository;
using LibraryWebApi.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApi.Service.Services
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BorrowedBookService(IBorrowedBookRepository borrowedBookRepository, IBookRepository bookRepository, IUserRepository userRepository)
        {
            _borrowedBookRepository = borrowedBookRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<BorrowedBook>> GetAllBorrowedBooksAsync()
        {
            return await _borrowedBookRepository.GetAllAsync();
        }

        public async Task<BorrowedBook> GetBorrowedBookByIdAsync(int id)
        {
            return await _borrowedBookRepository.GetByIdAsync(id);
        }

        public async Task AddBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            
            var book = await _bookRepository.GetBookByIdAsync(borrowedBook.BookID);
            var user = await _userRepository.GetUserByIdAsync(borrowedBook.UserID);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID {borrowedBook.BookID} not found.");
            if (user == null)
                throw new KeyNotFoundException($"User with ID {borrowedBook.UserID} not found.");

            
            borrowedBook.BookID = book.BookID;
            borrowedBook.UserID = user.UserID;

            await _borrowedBookRepository.AddAsync(borrowedBook);
        }

        public async Task UpdateBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            
            var book = await _bookRepository.GetBookByIdAsync(borrowedBook.BookID);
            var user = await _userRepository.GetUserByIdAsync(borrowedBook.UserID);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID {borrowedBook.BookID} not found.");
            if (user == null)
                throw new KeyNotFoundException($"User with ID {borrowedBook.UserID} not found.");

            
            borrowedBook.BookID = book.BookID;
            borrowedBook.UserID = user.UserID;

            await _borrowedBookRepository.UpdateAsync(borrowedBook);
        }

        public async Task DeleteBorrowedBookAsync(int id)
        {
            await _borrowedBookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BorrowedBook>> GetBorrowedBooksByBookTitleAsync(string title)
        {
            return await _borrowedBookRepository.GetByBookTitleAsync(title);
        }

        public async Task<IEnumerable<BorrowedBook>> GetBorrowedBooksByUserIdAsync(int userId)
        {
            return await _borrowedBookRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<BorrowedBook>> GetBorrowedBooksReturnDateByUserIdAsync(int userId)
        {
            
            return await _borrowedBookRepository.GetByUserIdWithReturnDateAsync(userId);

        }
        public async Task<IEnumerable<User>> GetUsersByBookIdAsync(int bookId)
        {
            return await _borrowedBookRepository.GetUsersByBookIdAsync(bookId);
        }

        
        public async Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId)
        {
            return await _borrowedBookRepository.GetBooksByUserIdAsync(userId);
        }

    }
}
