using LibraryWebApi.Domain.Models;
using LibraryWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound($"Book with ID {id} not found.");

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book cannot be null.");

            await _bookService.AddBookAsync(book);

            // Fetch the book again to ensure it has a valid ID
            var addedBook = await _bookService.GetBookByIdAsync(book.BookID);
            if (addedBook == null)
                return StatusCode(500, "An error occurred while creating the book.");

            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.BookID }, addedBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null || book.BookID != id)
                return BadRequest("Book data is invalid.");

            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
                return NotFound($"Book with ID {id} not found.");

            await _bookService.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
                return NotFound($"Book with ID {id} not found.");

            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooksByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Search title cannot be null or empty.");

            var books = await _bookService.SearchBooksByTitleAsync(title);
            return Ok(books);
        }
    }
}
