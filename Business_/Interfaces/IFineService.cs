using System;
using System.Collections.Generic;
using System.Text;
using Business_.DTOs.Fine_DTOs;
using Core.Entities;
namespace Business_.Interfaces
{
    internal interface IFineService
    {
        Task<bool> CreateFineAsync();
        Task<bool> UpdateFineAsync();
        Task<bool> PayFineAsync(PayFineDto dto);
    }
}
