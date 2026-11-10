using System;
using System.Collections.Generic;
using System.Text;
using Business_.DTOs.Fine_DTOs;
using Entity.Entities;
namespace LibraryManagement.Domain.Interfaces
{
    public interface IFineService
    {
        Task<bool> CreateFineAsync();
        Task<bool> UpdateFineAsync();
        Task<bool> PayFineAsync(int UserId,int UserBookId,PayFineDto dto);
        Task<List<ViewFineOutputDto>> ViewFineAsync(ViewFineDto dto);
    }
}
