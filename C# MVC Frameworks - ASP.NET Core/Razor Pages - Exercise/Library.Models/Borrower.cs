﻿namespace Library.Models
{
    using System.Collections.Generic;

    public class Borrower
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}