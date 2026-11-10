namespace LibraryManagement.Domain.Shared
{
    public class LibraryConsts
    {
        public int NameMaxLength = 255;
        public int EmailMaxLength = 255;
        public int PasswordMinLength = 8;
        public int ISBNLength = 13;
        public int BookBorrowTime = 7;
        public static decimal FineAmountPerDay = 5;
    }
}
