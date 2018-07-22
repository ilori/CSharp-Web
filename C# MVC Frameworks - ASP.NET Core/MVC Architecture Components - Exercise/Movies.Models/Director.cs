namespace Movies.Models
{
    using System.Collections.Generic;

    public class Director
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}