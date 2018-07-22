namespace Movies.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BorrowService : IBorrowService
    {
        private readonly MovieContext context;

        public BorrowService(MovieContext context)
        {
            this.context = context;
        }

        public async Task BorrowMovieAsync(Movie movie, DateTime borrowDate, DateTime? returnDate, int modelBorrowerId)
        {
            Borrower borrower = await this.context.Borrowers.SingleOrDefaultAsync(x => x.Id == modelBorrowerId);

            movie.Status = false;
            movie.Borrower = borrower;

            movie.Histories.Add(new History()
            {
                BorrowDate = borrowDate,
                ReturnDate = returnDate,
                BorrowerName = borrower.Name
            });

            await context.SaveChangesAsync();
        }
    }
}