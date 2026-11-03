
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities
{
    public class Book
    {
        public int Id { get;  set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public string Genre { get; set; }
        public int Count { get; set; }
        public string BookISBN { get; set; }
        public ICollection<User_Book> User_Book { get; set; }

    }
}
