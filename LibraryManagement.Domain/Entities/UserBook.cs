namespace LibraryManagement.Domain.Entities
{
    public class UserBook
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }

        public DateOnly BorrowDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public Fine Fine { get; set; }
        public DateOnly ReturnedDate { get; set; }
        public void MarkAsReturned()
        {
            IsReturned = true;
            ReturnedDate = DateOnly.FromDateTime(DateTime.Now);
        }

    }
}
