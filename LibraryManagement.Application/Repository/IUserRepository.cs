using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Repository
{
    public interface IUserRepository
    {
        User GetById(int id);
        IEnumerable<User> GetUser();
    }
}
