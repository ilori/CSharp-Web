namespace Library.App.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class SearchModel : PageModel
    {
        private readonly IAuthorService authorService;
        private readonly IBookService bookService;

        public SearchModel(IAuthorService authorService, IBookService bookService)
        {
            this.authorService = authorService;
            this.bookService = bookService;
        }

        public string SearchTerm { get; private set; }

        //TODO Reafactor and create ViewModels for Books and Authors because there are too much useless information

        public IEnumerable<Book> Books { get; private set; }

        public IEnumerable<Author> Authors { get; private set; }

        public async Task<IActionResult> OnGetAsync(string a)
        {
            this.SearchTerm = this.Request.QueryString.ToString().Split("=").Last();
            if (string.IsNullOrWhiteSpace(this.SearchTerm))
            {
                return this.RedirectToPage("/Index");
            }

            this.Books = await this.bookService.GetBooksBySearchTermAsync(this.SearchTerm);
            this.Authors = await this.authorService.GetAuthorsBySearchTermAsync(this.SearchTerm);
            return this.Page();
        }
    }
}