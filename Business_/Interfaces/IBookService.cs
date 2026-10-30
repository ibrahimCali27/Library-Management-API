using Business_.DTOs.Book_DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_.Interfaces
{
    public interface IBookService
    {
        Task<bool> BorrowBookAsync(int UserId, int BookId, BorrowBooksDto dto);
        Task<bool> ReturnBookAsync(ReturnBooksDto dto);
        Task<bool> AddBookAsync(AddBooksDto dto);
        Task<bool> DeleteBookAsync(DeleteBooksDto dto);
        Task<bool> UpdateBookAsync(int id, UpdateBookDto dto);
        Task<List<Book>> ViewBookAsync(ViewBookDto dto);
    }
}
