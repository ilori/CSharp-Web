namespace Movies.Services.Contracts
{

    using System.Threading.Tasks;
    using Models;

    public interface IHistoryService
    {

        Task<Movie> GetMovieHistoryByIdAsync(int id);

    }

}