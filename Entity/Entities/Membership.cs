using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public enum MembershipType
    {
        Basic = 1,
        Premium = 2,
        VIP = 3
    }
    public class Membership
    {
        public  int Id { get; set; }
        public MembershipType Type { get; set; }
        public int Borrow_Limit { get; set; }
        public float Price { get; set; }
        public ICollection<User_Membership> User_Memberships { get; set; }

    }
}
