using System;
using System.Collections.Generic;
using System.Text;
using Business_.DTOs.Fine_DTOs;
using Entity.Entities;
using LibraryManagement.Application.Contracts.DTOs.Fine_DTOs;
namespace Business_.Interfaces
{
    public interface IFineService
    {
        Task<bool> CreateFineAsync();
        Task<bool> UpdateFineAsync();
        Task<bool> PayFineAsync(int UserId,int UserBookId,PayFineDto dto);
        Task<List<Fine>> ViewFineAsync(ViewFineDto dto);
    }
}
