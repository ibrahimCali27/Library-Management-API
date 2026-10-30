using Business_.DTOs.Book_DTOs;
using Business_.Interfaces;
using Core.Entities;
using DataAccess.Context;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
namespace Business_.BusinessLayer.BLL
{
    public class BookService(DBContext context) : IBookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly DBContext _context = context;

        public async Task<bool> BorrowBookAsync(int UserId,int BookId,BorrowBooksDto dto)
        {

            var book = await _context.Books.FindAsync(BookId);
            var user = await _context.Users.FindAsync(UserId);
            if (book == null)
            {
                throw new Exception("This book doesn't exist.");
            }
            if (book.Count <= 0)
            {
                throw new Exception("This book is not in stock.");
            }
            book.Count--;
            var BorrowBook = new User_Book
            {
                User_Id = UserId,
                Book_Id = BookId,
                Borrow_Date = DateOnly.FromDateTime(DateTime.Now),
                Return_Date = DateOnly.FromDateTime(DateTime.Now).AddDays(7)
            };
            _context.User_Book.Add(BorrowBook);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReturnBookAsync(ReturnBooksDto dto)
        {
            var BorrowRecord = await _context.User_Book
                .FirstOrDefaultAsync(x =>
                    x.Book_Id == dto.BookId &&
                    x.User_Id == dto.UserId);

            if (BorrowRecord == null)
            {
                throw new Exception("This book was not borrowed by this user.");
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == dto.BookId);

            if (book != null)
            {
                book.Count++;
            }
            BorrowRecord.Returned_Date = DateOnly.FromDateTime(DateTime.Now);
            return await _context.SaveChangesAsync() > 0;
        }

        //Bu kısımda admin rolü gerekmektedir.
        public async Task<bool> AddBookAsync(AddBooksDto dto)
        {
            var ExistingBook = await _context.Books.FirstOrDefaultAsync(x => x.Name == dto.Name);
            if (ExistingBook != null)
            {
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
            var ExistingBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId) ?? throw new Exception("This book doesn't exist.");
            _context.Books.Remove(ExistingBook);  
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateBookAsync(int id,UpdateBookDto dto)
        {
            var ToBeUpdatedBook = await _context.Books.FindAsync(id);
            if (ToBeUpdatedBook == null)
            {
                throw new Exception("This book doesn't exist.");
            }
            bool noChanges =
            ToBeUpdatedBook.Name == dto.Name &&
            ToBeUpdatedBook.Writer == dto.Writer &&
            ToBeUpdatedBook.Genre == dto.Genre &&
            ToBeUpdatedBook.Count == dto.Count &&
            ToBeUpdatedBook.BookISMB == dto.BookISMB;

            if (noChanges)
            {
                throw new Exception("This book is the exact same.");
            }
            if(dto.Count < 0) { throw new Exception("Kitap sayısı negatif olamaz."); }
            ToBeUpdatedBook.Name = dto.Name;
            ToBeUpdatedBook.Writer = dto.Writer;
            ToBeUpdatedBook.Genre = dto.Genre;
            ToBeUpdatedBook.Count = dto.Count;
            ToBeUpdatedBook.BookISMB = dto.BookISMB;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Book>> ViewBookAsync(ViewBookDto dto)
        {
            var books = await _context.Books
                .Where(b => b.Genre == dto.Genre)
                .ToListAsync();

            if (books.Count == 0)
            {
                throw new Exception("No books found for this genre.");
            }

            return books;
        }
    }
}