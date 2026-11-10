using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_.Interfaces
{
    public interface IJWTService
    {
        public string GenerateToken(User user);
    }
}
