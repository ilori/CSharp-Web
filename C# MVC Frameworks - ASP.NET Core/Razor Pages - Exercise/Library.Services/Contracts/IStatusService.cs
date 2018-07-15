namespace Library.Services.Contracts
{
    using System.Threading.Tasks;
    using Models;

    public interface IStatusService
    {
        Task<Book> GetBookStatusAsync(int id);
    }
}