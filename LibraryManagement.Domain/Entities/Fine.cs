using LibraryManagement.Domain.Shared;

namespace LibraryManagement.Domain.Entities
{
    public class Fine
    {
        public int Id { get; set; }
        public int User_BookId { get; set; }
        public UserBook UserBook { get; set; }
        public decimal Price { get; set; }
        public DateOnly FineDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsPaid { get; set; }   

        public void BookIsReturned()
        {
            if (!IsReturned) { throw new BusinessException(LibraryDomainErrorCodes.BookNotReturned); }
        }
        public void UpdatePrice(UserBook borrow)
        {
            if (borrow.IsReturned)
            {
                return;
            }

            var overdueDays =
                DateOnly.FromDateTime(DateTime.Now).DayNumber -
                borrow.ReturnDate.DayNumber;

            if (overdueDays > 0)
            {
                Price = overdueDays *
                        LibraryConsts.FineAmountPerDay;
            }
        }

    } 
}
