namespace Movies.App.Models.ViewModels
{
    public class MovieDetailsViewModel
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        public int DirectorId { get; set; }

        public string Description { get; set; }

        public string YouTubeVideo { get; set; }

        public string CoverImage { get; set; }

        public bool Status { get; set; }
    }
}