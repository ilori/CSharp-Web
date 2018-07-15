namespace Library.App.Pages.Books
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;

    [BindProperties]
    public class AddModel : PageModel
    {
        private readonly IBookService service;

        public AddModel(IBookService service)
        {
            this.service = service;
        }


        [Required]
        [MinLength(5, ErrorMessage = "{0} must be atleast {1} symbols long!")]
        [MaxLength(25, ErrorMessage = "{0} cannot be more than {1} symbols long!")]
        public string Author { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "{0} must be atleast {1} symbols long!")]
        [MaxLength(16, ErrorMessage = "{0} cannot be more than {1} symbols long!")]
        public string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "{0} must be atleast {1} symbols long!")]
        [MaxLength(150, ErrorMessage = "{0} cannot be more than {1} symbols long!")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [DataType(DataType.Url)] public string CoverImage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            await this.service.AddBookAsync(this.Author, this.Title, this.Description, this.CoverImage);

            return this.RedirectToPage("/Index");
        }
    }
}