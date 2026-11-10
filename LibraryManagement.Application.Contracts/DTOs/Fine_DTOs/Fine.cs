using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Contracts.DTOs.Fine_DTOs
{
    public class Fine
    {
        public int Id { get; set; }
        public int UserBookId { get; set; }
        public float Price { get; set; }
        public bool IsReturned { get; set; }
        public bool IsPaid { get; set; }
    }
}
