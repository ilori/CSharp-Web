namespace Library.Services
{
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StatusService : IStatusService
    {
        private readonly LibraryContext context;

        public StatusService(LibraryContext context)
        {
            this.context = context;
        }

        public async Task<Book> GetBookStatusAsync(int id)
        {
            return await this.context.Books.Include(x => x.BookHistories).Include(x => x.Borrower)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}