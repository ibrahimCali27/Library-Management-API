using Business_.BusinessLayer.BLL;
using Business_.DTOs.Book_DTOs;
using Business_.DTOs.Fine_DTOs;
using Business_.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _BookService;

        public BookController(IBookService BookService)
        {
            _BookService = BookService;
        }

        [Authorize]
        [HttpPut("{UserId}/borrow-book/{BookId}")]
        public async Task<IActionResult> BorrowBookAsync(int UserId,int BookId,BorrowBooksDto dto)
        {
            var result = await _BookService.BorrowBookAsync(UserId,BookId,dto);

            if (!result)
                return BadRequest();

            return Ok("Book borrowed successfully.");
        }
        [Authorize]
        [HttpPut("return-book")]
        public async Task<IActionResult> ReturnBookAsync(ReturnBooksDto dto)
        {
            var result = await _BookService.ReturnBookAsync(dto);

            if (!result)
                return BadRequest();

            return Ok("Book returned successfully.");
        }
        /// <summary>
        /// Admin
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook(AddBooksDto dto)
        {
            var result = await _BookService.AddBookAsync(dto);

            if (!result)
                return BadRequest();

            return Ok("Book added successfully.");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-book")]
        public async Task<IActionResult> DeleteBookAsync(DeleteBooksDto dto)
        {
            var result = await _BookService.DeleteBookAsync(dto);

            if (!result)
                return BadRequest();

            return Ok("Book deleted successfully.");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("update-book/{id}")]
        public async Task<IActionResult> UpdateBooksAsync(int id,UpdateBookDto dto)
        {
            Console.WriteLine(dto.Name);
            var result = await _BookService.UpdateBookAsync(id,dto);

            if (!result)
                return BadRequest();

            return Ok("Book updated successfully.");
        }
        [Authorize]
        [HttpGet("view-books")]
        public async Task<IActionResult> ViewBook(ViewBookDto dto)
        {
            var Books = await _BookService.ViewBookAsync(dto);

            if (Books.Count != 0)
                return Ok(Books);

            return NotFound("No books found for this genre.");
        }
    }
}
