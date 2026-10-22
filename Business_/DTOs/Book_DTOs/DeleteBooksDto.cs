using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Book_DTOs
{
    public class DeleteBooksDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        public string BookName { get; set; }


    }
}
