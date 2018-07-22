namespace Movies.Services.Contracts
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IMovieService
    {

        Task AddMovieAsync(string title, string director, string description, string image);

        Task<List<Movie>> GetAllMoviesAsync();

        Task<Movie> GetMovieByIdAsync(int id);

        Task<bool> ContainsMovieAsync(int id);

        Task ReturnBookByIdAsync(int id);

        Task<List<Movie>> GetMoviesBySearchTermAsync(string searchTerm);

    }

}