using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Fine_DTOs
{
    public class PayFineDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int User_Id { get; set; }
        [Required]
        public float Payment { get; set; }
    }
}
