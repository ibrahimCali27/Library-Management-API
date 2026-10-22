using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User_Book
    {
        public int Id {  get; set; }
        public int Book_Id { get; set; }
        
        public int User_Id { get; set; }
        
        public DateOnly Borrow_Date { get; set; }
        public DateOnly Return_Date { get; set; }
        public bool Is_Returned { get; set; } = false;
        public DateOnly? Returned_Date { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public Fine Fine { get; set; }

    }
}
