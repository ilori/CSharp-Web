namespace Library.App.Pages.Books
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using Services.Contracts;

    public class StatusModel : PageModel
    {
        private readonly IStatusService service;

        public StatusModel(IStatusService service)
        {
            this.service = service;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.RedirectToPage("/Index");
            }

            this.Book = await this.service.GetBookStatusAsync(id.Value);

            if (this.Book == null)
            {
                return this.RedirectToPage("/Index");
            }

            return this.Page();
        }
    }
}