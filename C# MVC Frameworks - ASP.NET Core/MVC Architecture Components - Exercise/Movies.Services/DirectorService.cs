namespace Movies.Services
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class DirectorService : IDirectorService
    {

        private readonly MovieContext context;

        public DirectorService(MovieContext context)
        {
            this.context = context;
        }

        public async Task<Director> GetDirectorByIdAsync(int id)
        {
            return await this.context.Directors.Include(x => x.Movies).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Director>> GetDirectorsBySearchTermAsync(string searchTerm)
        {
            return await this.context.Directors.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();
        }

    }

}