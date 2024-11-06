using LibraryWebApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApi.Service.Interfaces
{
    public interface IBorrowedBookService
    {
        Task<IEnumerable<BorrowedBook>> GetAllBorrowedBooksAsync();
        Task<BorrowedBook> GetBorrowedBookByIdAsync(int id);
        Task AddBorrowedBookAsync(BorrowedBook borrowedBook);
        Task UpdateBorrowedBookAsync(BorrowedBook borrowedBook);
        Task DeleteBorrowedBookAsync(int id);

        // New methods
        Task<IEnumerable<BorrowedBook>> GetBorrowedBooksByBookTitleAsync(string title);
        Task<IEnumerable<BorrowedBook>> GetBorrowedBooksByUserIdAsync(int userId);
        Task<IEnumerable<BorrowedBook>> GetBorrowedBooksReturnDateByUserIdAsync(int userId);

        Task<IEnumerable<User>> GetUsersByBookIdAsync(int bookId); 
        Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId); 
    }
}
