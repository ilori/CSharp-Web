namespace Library.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BorrowerService : IBorrowerService
    {
        private readonly LibraryContext context;

        public BorrowerService(LibraryContext context)
        {
            this.context = context;
        }

        public async Task<bool> ContainsBorrowerAsync(string name)
        {
            return await this.context.Borrowers.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task AddBorrowerAsync(string name, string address)
        {
            await this.context.Borrowers.AddAsync(new Borrower()
            {
                Name = name,
                Address = address
            });

            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Borrower>> GetAllBorrowersAsync()
        {
            return await this.context.Borrowers.ToListAsync();
        }
    }
}