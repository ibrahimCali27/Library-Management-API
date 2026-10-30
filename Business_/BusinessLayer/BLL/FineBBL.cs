using Business_.DTOs.Fine_DTOs;
using Business_.Interfaces;
using Core.Entities;
using DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
namespace Business_.BusinessLayer.BLL
{
    public class FineService(DBContext context) : IFineService
    {
        private readonly DBContext? _context = context;
        private readonly ILogger<BookService> _logger;
        public async Task<bool> CreateFineAsync()
        {
            var borrowings = _context.User_Book.ToList();

            foreach (var borrowing in borrowings)
            {
                int dayDifference = DateOnly.FromDateTime(DateTime.Now).DayNumber - borrowing.Return_Date.DayNumber;
                if (!borrowing.Is_Returned && dayDifference == 1)
                {
                    bool fineExists = await _context.Fines
                .AnyAsync(f => f.User_BookId == borrowing.Id);

                    if (!fineExists)
                    {
                        var fine = new Fine
                        {
                            User_BookId = borrowing.Id,
                            Price = 2.5f,
                            FineDate = DateTime.Now,
                            IsReturned = false
                        };

                        _context.Fines.Add(fine);
                    }
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> PayFineAsync(PayFineDto dto)
        {
            var FineToPay = await _context.Fines.FirstOrDefaultAsync(x=>x.User_BookId == dto.Id);
            
            if( FineToPay == null) { throw new Exception("This fine doesn't exist."); }
            if (FineToPay.User_Book.User_Id != dto.User_Id) { throw new Exception("This isn't your fine."); }
            if(FineToPay.Price > dto.Payment) { throw new Exception("Not enough money."); }
            
            FineToPay.User_Book.Is_Returned = true;
            FineToPay.User_Book.Returned_Date = DateOnly.FromDateTime(DateTime.Now);
            FineToPay.IsReturned = true;
            
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

        public async Task<List<Fine>> ViewFineAsync(ViewFineDto dto)
        {
            var fines = await _context.Fines
                .Include(f => f.User_Book)
                .Where(f => f.User_Book != null &&
            f.User_Book.User_Id == dto.Id).ToListAsync();

            if (fines.Count == 0)
            {
                throw new Exception("No fines found for this user.");
            }

            return fines;
        }
    }
}
