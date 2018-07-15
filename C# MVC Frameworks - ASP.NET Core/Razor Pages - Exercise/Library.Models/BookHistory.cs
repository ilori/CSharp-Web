namespace Library.Models
{
    using System;

    public class BookHistory
    {
        public int Id { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}