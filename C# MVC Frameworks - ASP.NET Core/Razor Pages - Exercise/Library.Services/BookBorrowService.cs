namespace Library.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BookBorrowService : IBookBorrowService
    {
        private readonly LibraryContext context;

        public BookBorrowService(LibraryContext context)
        {
            this.context = context;
        }

        public async Task AddBookBorrowAsync(Book book, int borrowerId, DateTime borrowDate, DateTime? returnDate)
        {
            book.BorrowerId = borrowerId;
            book.Status = false;

            await context.BookHistories.AddAsync(new BookHistory()
            {
                Book = book,
                BorrowDate = borrowDate,
                ReturnDate = returnDate
            });

            await this.context.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(int bookId)
        {
            Book book = await this.context.Books.Include(x => x.BookHistories)
                .SingleOrDefaultAsync(x => x.Id == bookId);

            if (book.BookHistories.Last().ReturnDate == null)
            {
                book.BookHistories.Last().ReturnDate = DateTime.Now;
            }

            book.BorrowerId = null;
            book.Status = true;

            await this.context.SaveChangesAsync();
        }

        public bool IsBookAlreadyBorrowed(Book book)
        {
            return book.BorrowerId == null;
        }
    }
}