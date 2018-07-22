namespace Movies.Services.Contracts
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IDirectorService
    {

        Task<Director> GetDirectorByIdAsync(int id);
        
        Task<List<Director>> GetDirectorsBySearchTermAsync(string searchTerm);

    }

}