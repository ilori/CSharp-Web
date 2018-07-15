namespace Library.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBookService
    {
        Task AddBookAsync(string authorName, string title, string description, string image);

        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<Book> GetBookByIdAsync(int id);

        Task<IEnumerable<Book>> GetBooksBySearchTermAsync(string searchTerm);

        Task<bool> ContainsBookAsync(int id);
    }
}