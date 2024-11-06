using LibraryWebApi.Domain.Models;

namespace LibraryWebApi.Service.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int bookId);

        Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title);
    }
}
