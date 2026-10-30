using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities
{
    public enum MembershipType
    {
        Basic = 1,
        Premium = 2,
        VIP = 3,
        Admin = 4
    }
    public class Membership
    {
        public int Id { get; set; }
        public MembershipType Type { get; set; }
        public int Borrow_Limit { get; set; }
        public bool IsUnlimited { get; set; }
        public float Price { get; set; }
        public ICollection<User> User { get; set; }

    }
}
