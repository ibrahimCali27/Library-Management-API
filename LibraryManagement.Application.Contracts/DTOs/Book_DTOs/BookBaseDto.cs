using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryManagement.Application.Contracts.DTOs.Book_DTOs
{
    public class BookBaseDto
    {
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Genre { get; set; }
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
        [StringLength(13)]
        public string BookISBN { get; set; }
    }
    public class BookCreateDto : BookBaseDto { }
    public class BookUpdateDto : BookBaseDto 
    {
        public int Id { get; set; }
    }
}
