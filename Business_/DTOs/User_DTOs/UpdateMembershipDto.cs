using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.User_DTOs
{
    public class UpdateMembershipDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public MembershipType MembershipType { get; set; }

    }
}
