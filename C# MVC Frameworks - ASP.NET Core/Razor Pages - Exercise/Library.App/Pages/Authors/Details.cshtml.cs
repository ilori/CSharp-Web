namespace Library.App.Pages.Authors
{
    using System.Threading.Tasks;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    public class DetailsModel : PageModel
    {
        private readonly IAuthorService service;

        public DetailsModel(IAuthorService service)
        {
            this.service = service;
        }

        public Author Author { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.RedirectToPage("/Index");
            }

            this.Author = await this.service.GetAuthorByIdAsync(id.Value);

            if (this.Author == null)
            {
                return this.RedirectToPage("/Index");
            }

            return this.Page();
        }
    }
}