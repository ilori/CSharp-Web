namespace Library.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BookService : IBookService
    {
        private readonly LibraryContext context;

        public BookService(LibraryContext context)
        {
            this.context = context;
        }

        public async Task AddBookAsync(string authorName, string title, string description, string image)
        {
            if (await this.context.Authors.AnyAsync(x => x.Name == authorName) == false)
            {
                Author author = new Author()
                {
                    Name = authorName
                };

                Book book = new Book()
                {
                    Title = title,
                    Description = description,
                    CoverImage = image
                };

                author.Books.Add(book);

                await this.context.Authors.AddAsync(author);
                await this.context.SaveChangesAsync();
            }
            else
            {
                Author author = await this.context.Authors.SingleOrDefaultAsync(x => x.Name == authorName);

                Book book = new Book()
                {
                    Title = title,
                    Description = description,
                    CoverImage = image
                };

                author.Books.Add(book);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await this.context.Books.Include(x => x.Author).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await this.context.Books.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksBySearchTermAsync(string searchTerm)
        {
            return await this.context.Books.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower())).ToListAsync();
        }

        public async Task<bool> ContainsBookAsync(int id)
        {
            return await this.context.Books.AnyAsync(x => x.Id == id);
        }
    }
}