using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Shared;


public class BusinessException : Exception
{
    public string Code { get; }

    public BusinessException(string code)
    {
        Code = code;
    }
}