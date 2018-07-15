namespace Library.App.Pages.Books
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class ReturnModel : PageModel
    {
        private readonly IBookBorrowService service;

        public ReturnModel(IBookBorrowService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await this.service.ReturnBookAsync(id);

            return this.RedirectToPage("/Index");
        }
    }
}