using Business_.DTOs.Fine_DTOs;
using Business_.Interfaces;
using DataAccess.Library_DbContext;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
namespace Business_.BusinessLayer.BLL
{
    public class FineService(DBContext context,ILogger<FineService>logger) : IFineService
    {
        private readonly DBContext _context = context;
        private readonly ILogger<FineService> _logger = logger;

        public async Task<bool> CreateFineAsync()
        {
            var borrowings = await _context.User_Book.ToListAsync();
            bool IsFineCreated = false;
            foreach (var borrowing in borrowings)
            {
                int dayDifference = DateOnly.FromDateTime(DateTime.Now).DayNumber - borrowing.Return_Date.DayNumber;
                if (!borrowing.Is_Returned && dayDifference > 0)
                {
                    bool fineExists = await _context.Fines
                .AnyAsync(f => f.User_BookId == borrowing.Id);

                    if (!fineExists)
                    {
                        IsFineCreated = true;
                        var fine = new Fine
                        {
                            User_BookId = borrowing.Id,
                            Price = 2.5f,
                            FineDate = DateTime.Now,
                            IsReturned = false,
                            IsPaid = false
                        };

                        _context.Fines.Add(fine);
                    }
                }
            }
            if(IsFineCreated == false){
                _logger.LogInformation("Ceza yaratılmaya gerek görülmedi.");
                return false;
            }
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> PayFineAsync(int UserId,int UserBookId,PayFineDto dto)
        {
            var FineToPay = await _context.Fines.Include(x => x.User_Book).FirstOrDefaultAsync(x =>x.User_BookId == UserBookId && x.User_Book.UserId == UserId);

            if(FineToPay == null) { _logger.LogWarning("Bu ceza burada yok."); return false; }
            if(FineToPay.Price > dto.Payment) { _logger.LogWarning("Yetersiz bakiye."); return false; }

            if (!FineToPay.User_Book.Is_Returned) { _logger.LogWarning("Önce kitabı iade etmen gerek."); return false; }

            FineToPay.User_Book.Returned_Date = DateOnly.FromDateTime(DateTime.Now);
            FineToPay.IsPaid = true;
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateFineAsync()
        {
            var Fines = await _context.Fines
                .Include(f => f.User_Book)
                .ToListAsync();

            foreach (var Fine in Fines)
            {

                if (Fine.User_Book.Is_Returned)
                {
                    Fine.IsReturned = true;
                    continue;
                }

                int overdueDays = DateOnly.FromDateTime(DateTime.Now).DayNumber
                                - Fine.User_Book.Return_Date.DayNumber;

                if (overdueDays > 0)
                {
                    Fine.Price = overdueDays * 2.5f;
                    Fine.FineDate = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ViewFineOutputDto>> ViewFineAsync(ViewFineDto dto)
        {
            return await _context.Fines
                .Where(f => f.User_Book.UserId == dto.Id)
                .Select(f => new ViewFineOutputDto
                {
                    Id = f.Id,
                    Price = f.Price,
                    IsReturned = f.IsReturned,
                    User_BookId = f.User_BookId,
                    IsPaid = f.IsPaid
                })
                .ToListAsync();
        }
    }
}
