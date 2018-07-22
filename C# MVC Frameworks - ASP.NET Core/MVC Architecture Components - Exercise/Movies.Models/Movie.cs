namespace Movies.Models
{
    using System.Collections.Generic;

    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CoverImageUrl { get; set; }

        public Director Director { get; set; }
        public int DirectorId { get; set; }

        public Borrower Borrower { get; set; }
        public int? BorrowerId { get; set; }

        public ICollection<History> Histories { get; set; } = new List<History>();

        public bool Status { get; set; } = true;
    }
}