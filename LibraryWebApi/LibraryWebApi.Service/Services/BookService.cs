using LibraryWebApi.Domain.Models;
using LibraryWebApi.Repository;
using LibraryWebApi.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApi.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            // Business logic  With validations
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            
            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var existingBook = await _bookRepository.GetBookByIdAsync(book.BookID);
            if (existingBook == null)
                throw new KeyNotFoundException($"Book with ID {book.BookID} not found.");

            
            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found.");

            
            await _bookRepository.DeleteBookAsync(id);
        }
        public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title)
        {
            
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Search title cannot be null or empty.", nameof(title));

            
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}
