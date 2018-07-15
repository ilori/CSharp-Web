namespace Library.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IAuthorService
    {
        Task<Author> GetAuthorByIdAsync(int id);

        Task<IEnumerable<Author>> GetAuthorsBySearchTermAsync(string searchTerm);
    }
}