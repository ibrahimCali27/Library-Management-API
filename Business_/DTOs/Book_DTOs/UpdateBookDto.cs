using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Book_DTOs
{
    public class UpdateBookDto
    {
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Genre { get; set; }
        public int Count { get; set; }
        public string BookISMB { get; set; }
    }
}
