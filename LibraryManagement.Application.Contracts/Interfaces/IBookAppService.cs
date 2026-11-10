using Business_.DTOs.Book_DTOs;
using LibraryManagement.Application.Contracts.DTOs.Book_DTOs;
using LibraryManagement.Application.Contracts;
using LibraryManagement.Domain.Entities;

namespace Business_.Interfaces
{
    public interface IBookAppService
    {
        Task<ApiResult<UserBook>> BorrowBookAsync(int UserId, int BookId, BookBorrowDto dto);
        Task<ApiResult<int>> ReturnBookAsync(BookReturnDto dto);
        Task<ApiResult<int>> BookCreateAsync(BookCreateDto dto);
        Task<ApiResult<int>> DeleteBookAsync(Book dto);
        Task<ApiResult<bool>> UpdateBookAsync(int id, BookUpdateDto dto);
        Task<ApiResult<Book>> ViewBookAsync(BookListFilterDto dto);
        Task<ApiResult<Book>> ViewBorrowedBookAsync();
    }
}
