using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Business_.BusinessLayer.Interfaces
{
    internal interface IAdminService
    {
        Task<bool>AddBooks(IEnumerable<Book> books);
        Task<bool> RemoveBooks(IEnumerable<Book> books);
    }
}
