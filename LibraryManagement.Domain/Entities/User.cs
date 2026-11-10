using LibraryManagement.Domain.Shared;

namespace LibraryManagement.Domain.Entities
{
    public enum UserType
    {
        User = 0,
        Admin = 1
    }
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly RegisterDate { get; set; }
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }


        public void UserNullInfoCheck()
        {
            if (string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Surname) ||
                string.IsNullOrEmpty(PasswordHash))
            {
                throw new BusinessException(LibraryDomainErrorCodes.UserNullInputs);
            }
        }
        public void PasswordCheck()
        {
            if(PasswordHash.Length < 8)
            {
                throw new BusinessException(LibraryDomainErrorCodes.PasswordShortLength);
            }
        }
        public void CheckBorrowLimit(int activeBorrowCount)
        {
            if (UserBooks.Count >= activeBorrowCount)
            {
                throw new BusinessException(
                    LibraryDomainErrorCodes.BorrowLimitExceeded);
            }
        }
    }
}
    

