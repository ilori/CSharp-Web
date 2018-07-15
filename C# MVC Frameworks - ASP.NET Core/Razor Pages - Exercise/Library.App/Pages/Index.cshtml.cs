namespace Library.App.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using Services.Contracts;

    public class IndexModel : PageModel
    {
        private readonly IBookService service;

        public IndexModel(IBookService service)
        {
            this.service = service;
        }

        public IEnumerable<Book> Books { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            this.Books = await this.service.GetAllBooksAsync();

            return this.Page();
        }
    }
}