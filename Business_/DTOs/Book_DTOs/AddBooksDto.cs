using Business_.DTOs.User_DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Business_.DTOs.Book_DTOs
{
    public class AddBooksDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Writer { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
        [Required]
        [StringLength(13)]
        public string BookISBN { get; set; }
    }
}
