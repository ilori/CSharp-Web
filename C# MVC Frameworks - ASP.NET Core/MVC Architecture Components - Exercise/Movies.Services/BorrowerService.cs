namespace Movies.Services
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BorrowerService : IBorrowerService
    {

        private readonly MovieContext context;

        public BorrowerService(MovieContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Borrower>> GetAllBorrowersAsync()
        {
            return await this.context.Borrowers.ToListAsync();
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

        public async Task<bool> ContainsBorrower(string name)
        {
            return await this.context.Borrowers.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

    }

}