using LibraryWebApi.Domain.Models;
using LibraryWebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApi.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowedBookController : ControllerBase
    {
        private readonly IBorrowedBookService _borrowedBookService;

        public BorrowedBookController(IBorrowedBookService borrowedBookService)
        {
            _borrowedBookService = borrowedBookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrowedBooks()
        {
            var borrowedBooks = await _borrowedBookService.GetAllBorrowedBooksAsync();
            return Ok(borrowedBooks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowedBookById(int id)
        {
            var borrowedBook = await _borrowedBookService.GetBorrowedBookByIdAsync(id);
            if (borrowedBook == null)
                return NotFound($"BorrowedBook with ID {id} not found.");

            return Ok(borrowedBook);
        }

        [HttpPost]
        public async Task<IActionResult> AddBorrowedBook([FromBody] BorrowedBook borrowedBook)
        {
            if (borrowedBook == null)
                return BadRequest("BorrowedBook cannot be null.");

            await _borrowedBookService.AddBorrowedBookAsync(borrowedBook);
            return CreatedAtAction(nameof(GetBorrowedBookById), new { id = borrowedBook.BorrowedID }, borrowedBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrowedBook(int id, [FromBody] BorrowedBook borrowedBook)
        {
            if (borrowedBook == null || borrowedBook.BorrowedID != id)
                return BadRequest("BorrowedBook data is invalid.");

            var existingBorrowedBook = await _borrowedBookService.GetBorrowedBookByIdAsync(id);
            if (existingBorrowedBook == null)
                return NotFound($"BorrowedBook with ID {id} not found.");

            await _borrowedBookService.UpdateBorrowedBookAsync(borrowedBook);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowedBook(int id)
        {
            var existingBorrowedBook = await _borrowedBookService.GetBorrowedBookByIdAsync(id);
            if (existingBorrowedBook == null)
                return NotFound($"BorrowedBook with ID {id} not found.");

            await _borrowedBookService.DeleteBorrowedBookAsync(id);
            return NoContent();
        }

        [HttpGet("book/{title}")]
        public async Task<IActionResult> GetBorrowedBooksByBookTitle(string title)
        {
            var borrowedBooks = await _borrowedBookService.GetBorrowedBooksByBookTitleAsync(title);
            return Ok(borrowedBooks);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBorrowedBooksByUserId(int userId)
        {
            var borrowedBooks = await _borrowedBookService.GetBorrowedBooksByUserIdAsync(userId);
            return Ok(borrowedBooks);
        }

        [HttpGet("return-date/user/{userId}")]
        public async Task<IActionResult> GetBorrowedBooksReturnDateByUserId(int userId)
        {
            var borrowedBooks = await _borrowedBookService.GetBorrowedBooksReturnDateByUserIdAsync(userId);
            return Ok(borrowedBooks);
        }

        [HttpGet("users-by-book/{bookId}")]
        public async Task<IActionResult> GetUsersByBookId(int bookId)
        {
            var users = await _borrowedBookService.GetUsersByBookIdAsync(bookId);
            if (!users.Any())
                return NotFound($"No users found who borrowed the book with ID {bookId}.");

            return Ok(users);
        }

        [HttpGet("books-by-user/{userId}")]
        public async Task<IActionResult> GetBooksByUserId(int userId)
        {
            var books = await _borrowedBookService.GetBooksByUserIdAsync(userId);
            if (!books.Any())
                return NotFound($"No books found for the user with ID {userId}.");

            return Ok(books);
        }
    }
}
