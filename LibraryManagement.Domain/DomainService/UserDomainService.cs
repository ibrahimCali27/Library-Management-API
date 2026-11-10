using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void UserNotChanged(User user)
        {
            var User
            var NotChanged = user.Name
        }
    }
}