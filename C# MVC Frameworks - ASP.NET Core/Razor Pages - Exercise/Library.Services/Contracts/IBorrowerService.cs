namespace Library.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBorrowerService
    {
        Task<bool> ContainsBorrowerAsync(string name);

        Task AddBorrowerAsync(string name, string address);

        Task<IEnumerable<Borrower>> GetAllBorrowersAsync();
    }
}