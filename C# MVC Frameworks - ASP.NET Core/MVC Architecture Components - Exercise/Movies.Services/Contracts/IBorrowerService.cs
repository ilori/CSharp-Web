namespace Movies.Services.Contracts
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBorrowerService
    {

        Task<IEnumerable<Borrower>> GetAllBorrowersAsync();

        Task AddBorrowerAsync(string name, string address);

        Task<bool> ContainsBorrower(string name);

    }

}