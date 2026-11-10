using System;
using System.Collections.Generic;
using System.Text;

namespace Business_.DTOs.Book_DTOs
{
    public class BookListFilterDto
    {       
            public int BookId { get; set; }
            public string Name { get; set; }
            public string Writer { get; set; }
            public string Genre { get; set; }
            public int Count { get; set; }     
    }
}
