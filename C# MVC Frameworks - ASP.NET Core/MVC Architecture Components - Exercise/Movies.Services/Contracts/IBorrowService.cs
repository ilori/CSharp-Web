namespace Movies.Services.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IBorrowService
    {
        Task BorrowMovieAsync(Movie movie, DateTime borrowDate, DateTime? returnDate, int modelBorrowerId);
    }
}