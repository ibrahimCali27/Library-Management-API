using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities
{
    public class User_Book
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }

        public DateOnly Borrow_Date { get; set; }
        public DateOnly Return_Date { get; set; }
        public bool Is_Returned { get; set; } = false;
        public Fine Fine { get; set; }
        public DateOnly? Returned_Date { get; set; }
        
        
        

    }
}
