using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IJWTService
    {
        public string GenerateToken(User user);
    }
}
