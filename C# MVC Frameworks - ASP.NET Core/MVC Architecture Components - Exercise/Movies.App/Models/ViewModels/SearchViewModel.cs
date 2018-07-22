namespace Movies.App.Models.ViewModels
{

    using System.Collections.Generic;
    using Movies.Models;

    public class SearchViewModel
    {

        public List<Movie> Movies { get; set; }

        public List<Director> Directors { get; set; }

        public string SearchTerm { get; set; }

    }

}