using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Book_DTOs
{
    public class ReturnBooksDto
    {
        [Required]
        public int UserBookId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        
    }
}
