namespace Library.Services.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IBookBorrowService
    {
        Task AddBookBorrowAsync(Book book, int borrowerId, DateTime borrowDate, DateTime? returnDate);

        Task ReturnBookAsync(int bookId);
        bool IsBookAlreadyBorrowed(Book book);
    }
}