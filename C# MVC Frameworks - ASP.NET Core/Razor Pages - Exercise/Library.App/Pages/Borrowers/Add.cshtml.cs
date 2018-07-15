using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.App.Pages.Borrowers
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    [BindProperties]
    public class AddModel : PageModel
    {
        private readonly IBorrowerService service;

        public AddModel(IBorrowerService service)
        {
            this.service = service;
        }

        [Required]
        [MinLength(6, ErrorMessage = "{0} must be atleast {1} symbols long")]
        [MaxLength(18, ErrorMessage = "{0} cannot be more than {1} symbols long")]
        public string Name { get; set; }


        [Required]
        [MinLength(7, ErrorMessage = "{0} must be atleast {1} symbols long")]
        [MaxLength(31, ErrorMessage = "{0} cannot be more than {1} symbols long")]
        public string Address { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (this.ModelState.IsValid == false)
            {
                return this.Page();
            }

            if (await this.service.ContainsBorrowerAsync(this.Name))
            {
                this.ModelState.AddModelError(this.Name, "Name already exist!");
                return this.Page();
            }

            await this.service.AddBorrowerAsync(this.Name, this.Address);

            return this.RedirectToPage("/Index");
        }
    }
}