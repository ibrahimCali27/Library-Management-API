using Business_.DTOs.Book_DTOs;
using Business_.Interfaces;
using Core.Entities;
using DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Business_.BusinessLayer.BLL
{
    [ApiController]
    public class BookService(DBContext context) : IBookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly DBContext _context = context;

        public async Task<bool> BorrowBookAsync(BorrowBooksDto dto)
        {
            var book = await _context.Books
        .FirstOrDefaultAsync(x => x.Id == dto.BookId);

            if (book == null)
            {
                _logger.LogWarning("Book not found. BookId: {BookId}", dto.BookId);
                throw new Exception("This book doesn't exist.");
            }
            if (book.Count <= 0)
            {
                _logger.LogWarning("Book not found. BookId: {BookId}", dto.BookId);
                throw new Exception("This book is not in stock.");
            }
            book.Count--;
            var BorrowBook = new User_Book
            {
                User_Id = dto.UserId,
                Book_Id = dto.BookId,
                Borrow_Date = DateOnly.FromDateTime(DateTime.Now),
                Return_Date = DateOnly.FromDateTime(DateTime.Now).AddDays(7)
            };
            _context.User_Book.Add(BorrowBook);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReturnBookAsync(ReturnBooksDto dto)
        {
            var borrowedBook = await _context.User_Book
                .FirstOrDefaultAsync(x =>
                    x.Book_Id == dto.BookId &&
                    x.User_Id == dto.UserId);

            if (borrowedBook == null)
            {
                _logger.LogWarning(
                    "Borrow record not found. BookId: {BookId}, UserId: {UserId}",
                    dto.BookId,
                    dto.UserId);

                throw new Exception("This book was not borrowed by this user.");
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == dto.BookId);

            if (book != null)
            {
                book.Count++;
            }
            borrowedBook.Returned_Date = DateOnly.FromDateTime(DateTime.Now);
            return await _context.SaveChangesAsync() > 0;
        }

        //Bu kısımda admin rolü gerekmektedir.
        public async Task<bool> AddBookAsync(AddBooksDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if (user == null)
            {
                _logger.LogWarning("User not found");
                throw new Exception("User not found.");
            }
            var userType = user.UserType;
            if (userType != UserType.Admin) 
            {
                _logger.LogWarning("User forbidden to add books. UserId: {UserId}", dto.UserId);
                throw new Exception("You are not allowed to do this.");
            }
            var ExistingBook = await _context.Books.FirstOrDefaultAsync(x => x.Name == dto.Name);
            if (ExistingBook != null)
            {
                _logger.LogWarning("Book already exists. BookName: {Name}", dto.Name);
                throw new Exception("This book already exists."); 
            }
            var Book = new Book
            {
                Name = dto.Name,
                Writer =dto.Writer,
                Genre = dto.Genre,
                Count = dto.Count,
                BookISMB=dto.BookISMB
            };
            _context.Books.Add(Book);

            return await _context.SaveChangesAsync() > 0;
        }
        

        public async Task<bool> DeleteBookAsync(DeleteBooksDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if (user == null)
            {
                _logger.LogWarning("User not found");
                throw new Exception("User not found.");
            }
            var userType = user.UserType;
            if (userType != UserType.Admin)
            {
                _logger.LogWarning("User is forbidden to add books. UserId: {UserId}", dto.UserId);
                throw new Exception("You are not allowed to do this.");
            }
            var ExistingBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId);
            if (ExistingBook == null) 
            {
                _logger.LogWarning("Book doesn't exist. BookId: {BookId}", dto.BookId);
                throw new Exception("This book doesn't exist.");
            } 
            _context.Books.Remove(ExistingBook);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}