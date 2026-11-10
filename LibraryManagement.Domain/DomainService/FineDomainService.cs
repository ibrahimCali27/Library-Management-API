using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace LibraryManagement.Domain.DomainService
{
    public class FineDomainService
    {
        public void UpdateFine(Fine Fine)
        {
            if (!Fine.IsReturned)
            {
                var days =
    DateOnly.FromDateTime(DateTime.Now).DayNumber -
    Fine.UserBook.ReturnDate.DayNumber;
                Fine.Price = LibraryConsts.FineAmountPerDay * days;
            }

        }
        public void PayFine(Fine Fine) { }

    }
}
