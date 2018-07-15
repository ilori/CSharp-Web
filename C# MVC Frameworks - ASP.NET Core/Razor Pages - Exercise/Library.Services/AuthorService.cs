namespace Library.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext context;

        public AuthorService(LibraryContext context)
        {
            this.context = context;
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await this.context.Authors.Include(x => x.Books).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsBySearchTermAsync(string searchTerm)
        {
            return await this.context.Authors.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToListAsync();
        }
    }
}