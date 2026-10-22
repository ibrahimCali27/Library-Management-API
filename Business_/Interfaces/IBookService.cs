using Business_.DTOs.Book_DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_.Interfaces
{
    internal interface IBookService
    {
        Task<bool> BorrowBookAsync(BorrowBooksDto dto);
        Task<bool> ReturnBookAsync(ReturnBooksDto dto);
        Task<bool> AddBookAsync(AddBooksDto dto);
        Task<bool> DeleteBookAsync(DeleteBooksDto dto);

    }
}
