namespace LibraryManagement.Domain.Entities
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
        public int BorrowLimit { get; set; }
        public bool IsUnlimited { get; set; }
        public decimal Price { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
