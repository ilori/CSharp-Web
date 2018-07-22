namespace Movies.Models
{
    using System;

    public class History
    {
        public int Id { get; set; }

        public Movie Movie { get; set; }
        public int MovieId { get; set; }

        public string BorrowerName { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}