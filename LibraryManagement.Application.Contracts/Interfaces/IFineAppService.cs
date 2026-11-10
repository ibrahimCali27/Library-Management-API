using Business_.DTOs.Book_DTOs;
using LibraryManagement.Application.Contracts.DTOs.Book_DTOs;
using LibraryManagement.Application.Contracts;
using LibraryManagement.Domain.Entities;
using Business_.DTOs.Fine_DTOs;
namespace Business_.Interfaces
{
    public interface IFineService
    {
        Task<ApiResult<Fine>> CreateFineAsync();
        Task<ApiResult<Fine>> UpdateFineAsync();
        Task<ApiResult<Fine>> PayFineAsync(int UserId,int UserBookId,PayFineDto dto);
        Task<List<Fine>> ViewFineAsync(ViewFineDto dto);
    }
}
