namespace Movies.Services
{

    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HistoryService : IHistoryService
    {

        private readonly MovieContext context;

        public HistoryService(MovieContext context)
        {
            this.context = context;
        }

        public async Task<Movie> GetMovieHistoryByIdAsync(int id)
        {
            return await this.context.Movies.Include(x => x.Histories).SingleOrDefaultAsync(x => x.Id == id);
        }

    }

}