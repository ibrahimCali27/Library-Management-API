
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_.DTOs.Fine_DTOs
{
    public class ViewFineOutputDto
    {
        public int Id { get; set; }
        public int User_BookId { get; set; }
        public float Price { get; set; }
        public bool IsReturned { get; set; }
        public bool IsPaid { get; set; }
    }
}
