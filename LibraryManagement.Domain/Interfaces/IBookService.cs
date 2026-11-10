namespace LibraryManagement.Domain.Interfaces
{
    public interface IBookService
    {
        Task<bool> BorrowBookAsync(int UserId, int BookId, BookBorrowDto dto);
        Task<bool> ReturnBookAsync(BookReturnDto dto);
        Task<bool> AddBookAsync(AddBooksDto dto);
        Task<bool> DeleteBookAsync(BookDeleteDto dto);
        Task<bool> UpdateBookAsync(int id, UpdateBookDto dto);
        Task<List<Book>> ViewBookAsync(ViewBookDto dto);
        Task<List<User_Book>> ViewBorrowedBookAsync();
    }
}
