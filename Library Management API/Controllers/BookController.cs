using Business_.BusinessLayer.BLL;
using Business_.DTOs.Book_DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController:ControllerBase
    {
        private readonly BookService _BookService;
        public BookController(BookService BookService)
        {
            _BookService = BookService;
        }
        [Authorize]
        [HttpPut("borrow-book")]
        public async Task<IActionResult> BorrowBookAsync(BorrowBooksDto dto)
        {
            var result = await _BookService.BorrowBookAsync(dto);

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
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBooksDto dto)
        {
            await _BookService.AddBookAsync(dto);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> DeleteBookAsync(DeleteBooksDto dto)
        {
            var result = await _BookService.DeleteBookAsync(dto);

            if (!result)
                return BadRequest();

            return Ok("Book deleted successfully.");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateBooksAsync(UpdateBookDto dto)
        {
            var result = await _BookService.UpdateBookAsync(dto);

            if (!result)
                return BadRequest();

            return Ok("Book updated successfully.");
        }
    }
}
