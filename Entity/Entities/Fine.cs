
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities
{
    public class Fine
    {
        public int Id { get; set; }
        public int User_BookId { get; set; }
        public User_Book User_Book { get; set; }
        public float Price { get; set; }
        public DateTime FineDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsPaid { get; set; }
        

    }
}
