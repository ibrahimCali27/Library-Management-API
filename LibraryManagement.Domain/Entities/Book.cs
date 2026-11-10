using LibraryManagement.Domain.Shared;

namespace LibraryManagement.Domain.Entities
{
    public class Book
    {
        public int Id { get;  set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Genre { get; set; }
        public int Count { get; set; }
        public string BookISBN { get; set; }
        public ICollection<UserBook> UserBook { get; set; }
        
        
        public void BookBorrow()
        {
            if (Count <= 0) { throw new BusinessException(LibraryDomainErrorCodes.BookOutOfStock);}
            Count=Count-1;
        }
        public void ReturnBook()
        {
            Count++;
        }
        public void CheckRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Genre) ||
                string.IsNullOrWhiteSpace(Writer) ||
                string.IsNullOrWhiteSpace(BookISBN))
            {
                throw new BusinessException(
                    LibraryDomainErrorCodes.BookNullInputs);
            }
        }
    }
}
