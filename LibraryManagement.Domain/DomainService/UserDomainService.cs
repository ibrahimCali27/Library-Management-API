using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Shared;
namespace LibraryManagement.Domain.DomainService
{
    public class UserDomainService
    {
        public void UserNullCheck(User user)
        {
            if (user == null) { throw new BusinessException(LibraryDomainErrorCodes.UserNullInputs); }
        }
        public void UserNullInput(User user)
        {
            {
                if (user.Email == null || user.PasswordHash == null) { throw new BusinessException(LibraryDomainErrorCodes.IncorrectInputs); }
            }
        }
        public void UserNotChanged(User CurrentUser,User UpdatedUser)
        {
           
            var NotChanged = CurrentUser.Name == UpdatedUser.Name&&
                            CurrentUser.Email==UpdatedUser.Email&&
                            CurrentUser.PasswordHash==UpdatedUser.PasswordHash&&
                            CurrentUser.Surname==UpdatedUser.Surname&&
                            CurrentUser.MembershipId==UpdatedUser.MembershipId;
            if (NotChanged) {throw new BusinessException(LibraryDomainErrorCodes.UserNotChanged); }
        }
    }
}