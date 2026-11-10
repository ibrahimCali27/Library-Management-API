using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Book_DTOs
{
    public class BookReturnDto
    {

        public int UserBookId { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }
        
    }
}
