using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User_Membership
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int MembershipId { get; set; }
        public Membership Membership { get; set; }

        public DateTime Date { get; set; }
    }
}
