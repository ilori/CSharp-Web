namespace Library.Models
{
    using System.Collections.Generic;

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CoverImage { get; set; }

        public bool Status { get; set; } = true;

        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public Borrower Borrower { get; set; }
        public int? BorrowerId { get; set; }

        public ICollection<BookHistory> BookHistories { get; set; } = new List<BookHistory>();
    }
}