namespace Library.App.Pages.Books
{
    using System.Threading.Tasks;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class DetailsModel : PageModel
    {
        private readonly IBookService service;

        public DetailsModel(IBookService service)
        {
            this.service = service;
        }

        public Book Book { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.RedirectToPage("/Index");
            }

            this.Book = await this.service.GetBookByIdAsync(id.Value);

            if (this.Book == null)
            {
                return this.RedirectToPage("/Index");
            }

            return this.Page();
        }

        public void OnGetReturn(int id)
        {
            ;
        }
    }
}