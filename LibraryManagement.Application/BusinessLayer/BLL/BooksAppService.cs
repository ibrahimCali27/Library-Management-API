using LibraryManagement.EntityFrameworkCore.Library_DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Entity;
namespace LibraryManagement.Application.BusinessLayer.BLL
{
    public class BookService(LibraryDbContext context,ILogger<BookService> logger) : IBookAppService
    {
        private readonly ILogger<BookService> _logger = logger;
        private readonly LibraryDbContext _context = context;

        public async Task<bool> BorrowBookAsync(int UserId,int BookId,BorrowBooksDto dto)
        {

            var book = await _context.Books.FindAsync(BookId);
            var user = await _context.Users
    .Include(x => x.Membership)
    .FirstOrDefaultAsync(x => x.Id == UserId);

            if (book == null)
            {
                _logger.LogWarning("Bu kitap kütüphanede yoktur.");
                return false;
            }
            if (user == null) { _logger.LogWarning("Böyle bir kullanıcı yoktur."); return false; }
            if (book.Count <= 0)
            {
                _logger.LogWarning("Bu kitap stoklarda yoktur.");
                    return false;
            }
            var activeBorrowCount = await _context.User_Book
    .CountAsync(x =>
        x.UserId == UserId &&
        !x.IsReturned);

            if (activeBorrowCount >= user.Membership.BorrowLimit)
            {
                _logger.LogWarning("Daha fazla kitap ödünç alamazsın.");
                return false;
            }

            book.Count--;
            
            var userBook = new UserBook
            {
                UserId = UserId,
                BookId = BookId,
                Borrow_Date = DateOnly.FromDateTime(DateTime.Now),
                Return_Date = DateOnly.FromDateTime(DateTime.Now).AddDays(7)
            };
            _context.User_Book.Add(userBook);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReturnBookAsync(ReturnBooksDto dto)
        {
            var borrowRecord = await _context.User_Book
                .FirstOrDefaultAsync(x =>
                    x.BookId == dto.BookId &&
                    x.UserId == dto.UserId &&
                    !x.IsReturned);

            if (borrowRecord == null)
            {
                _logger.LogWarning("Aktif ödünç kaydı bulunamamıştır.");
                return false;
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == dto.BookId);

            if (book != null)
            {
                book.Count++;
            }

            borrowRecord.Is_Returned = true;
            borrowRecord.Returned_Date = DateOnly.FromDateTime(DateTime.Now);

            return await _context.SaveChangesAsync() > 0;
        }

        //Bu kısımda admin rolü gerekmektedir.
        public async Task<bool> AddBookAsync(AddBooksDto dto)
        {
            var ExistingBook = await _context.Books.FirstOrDefaultAsync(x => x.Name == dto.Name);
            if (ExistingBook != null)
            {
                _logger.LogWarning("Bu kitap çoktan eklenmiştir.");
                return false;
            }
            var Book = new Book
            {
                Name = dto.Name,
                Writer =dto.Writer,
                Genre = dto.Genre,
                Count = dto.Count,
                BookISBN=dto.BookISBN
            };
            _context.Books.Add(Book);

            return await _context.SaveChangesAsync() > 0;
        }
        

        public async Task<bool> DeleteBookAsync(DeleteBooksDto dto)
        {
            var ExistingBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId);
                if (ExistingBook == null){
                _logger.LogWarning("Bu kitap kütüphanede yoktur."); 
                return false; }
            _context.Books.Remove(ExistingBook);  
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateBookAsync(int id,UpdateBookDto dto)
        {
            var ToBeUpdatedBook = await _context.Books.FindAsync(id); 
                if(ToBeUpdatedBook==null){
                _logger.LogWarning("Bu kitap kütüphanede yoktur.");
                return false;
            }
            bool noChanges =
            ToBeUpdatedBook.Name == dto.Name &&
            ToBeUpdatedBook.Writer == dto.Writer &&
            ToBeUpdatedBook.Genre == dto.Genre &&
            ToBeUpdatedBook.Count == dto.Count &&
            ToBeUpdatedBook.BookISBN == dto.BookISBN;

            if (noChanges)
            {
                _logger.LogWarning("Bu kitap kütüphanedeki ile aynıdır.");
                return false;
            }
            if(dto.Count < 0) { _logger.LogWarning("Kitap sayısı negatif olamaz."); return false; }
            ToBeUpdatedBook.Name = dto.Name;
            ToBeUpdatedBook.Writer = dto.Writer;
            ToBeUpdatedBook.Genre = dto.Genre;
            ToBeUpdatedBook.Count = dto.Count;
            ToBeUpdatedBook.BookISBN = dto.BookISBN;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Book>?> ViewBookAsync(ViewBookDto dto)
        {
            var books = await _context.Books
                .Where(b => b.Genre == dto.Genre)
                .ToListAsync();
            Console.WriteLine(dto.Genre);
            if (books.Count == 0)
            {
                _logger.LogWarning("Bu kategoride kitap yoktur.");
                return null;
            }

            return books;
        }

        public async Task<List<User_Book>?> ViewBorrowedBookAsync()
        {
            var BorrowedBooks = await _context.User_Book.Where(x => x.Is_Returned == false).ToListAsync();
            if (BorrowedBooks.Count == 0)
            {
                _logger.LogWarning("Hiçbir kitap ödünç alınmamıştır.");
                return null;
            }
            return BorrowedBooks;
        }
    }
}