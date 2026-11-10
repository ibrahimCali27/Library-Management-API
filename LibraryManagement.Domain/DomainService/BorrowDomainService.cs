using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Shared;

namespace LibraryManagement.Domain.DomainService
{
    public class BorrowDomainService
    {
        public UserBook Borrow(User user,Book book,int activeBorrowCount)
        {
            user.CheckBorrowLimit(activeBorrowCount);

            book.BookBorrow();

            return new UserBook
            {
                UserId = user.Id,
                BookId = book.Id,
                BorrowDate = DateOnly.FromDateTime(DateTime.Now),
                ReturnDate = DateOnly.FromDateTime(DateTime.Now).AddDays(7)
            };
        }
        public void Return(User user, UserBook Borrow)
        {
            if (user.Id != Borrow.User.Id)
            {
                throw new BusinessException(LibraryDomainErrorCodes.BorrowRecordNotFound);
            }
            Borrow.MarkAsReturned();
            Borrow.Book.ReturnBook();

            if (Borrow.Fine != null)
            {
                    Borrow.Fine.FineDate =Borrow.ReturnedDate;
                    Borrow.Fine.IsReturned = true;
            }
        }
    }
}