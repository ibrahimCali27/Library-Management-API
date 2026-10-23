using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Fine_DTOs
{
    public class CreateFineDto
    {
        [Required]
        public DateOnly ReturnDate;
        [Required]
        public int UserId;
        [Required]
        public int BookId;
    }
}
