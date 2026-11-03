using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Fine_DTOs
{
    public class PayFineDto
    {
        [Required]
        public float Payment { get; set; }
    }
}
