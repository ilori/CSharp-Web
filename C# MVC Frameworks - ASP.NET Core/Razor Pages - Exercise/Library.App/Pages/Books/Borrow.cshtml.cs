namespace Library.App.Pages.Books
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Contracts;

    [BindProperties(SupportsGet = true)]
    public class BorrowModel : PageModel
    {
        private readonly IBookService service;
        private readonly IBorrowerService borrowerService;
        private readonly IBookBorrowService bookBorrowService;

        public BorrowModel(IBookService service, IBorrowerService borrowerService, IBookBorrowService bookBorrowService)
        {
            this.service = service;
            this.borrowerService = borrowerService;
            this.bookBorrowService = bookBorrowService;
        }

        public Book Book { get; set; }

        public string Borrower { get; set; }

        public List<SelectListItem> Borrowers { get; set; } = new List<SelectListItem>();

        public string BorrowDate { get; set; }

        public string ReturnDate { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, string error = null)
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


            if (!this.bookBorrowService.IsBookAlreadyBorrowed(this.Book))
            {
                return this.RedirectToPage("/Index");
            }


            this.Borrowers.AddRange(this.borrowerService.GetAllBorrowersAsync().Result.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }));

            if (error != null)
            {
                this.ModelState.AddModelError("error", error);
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (this.Borrower == null)
            {
                return this.RedirectToPage(new
                {
                    id,
                    error = "Please Add a Borrower before trying to borrow a book !"
                });
            }

            if (!DateTime.TryParseExact(this.BorrowDate, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime borrowDate))
            {
                if (this.BorrowDate == null)
                {
                    borrowDate = DateTime.Now;
                }
                else
                {
                    return this.RedirectToPage(new
                    {
                        id,
                        error = "Invalid Date, please try again!"
                    });
                }
            }

            DateTime? returnDate = null;

            if (this.ReturnDate != null)
            {
                if (!DateTime.TryParseExact(this.ReturnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime test))
                {
                    return this.RedirectToPage(new
                    {
                        id,
                        error = "Invalid Date, please try again!"
                    });
                }

                returnDate = test;
            }

            if (borrowDate > returnDate)
            {
                return this.RedirectToPage(new
                {
                    id,
                    error = "Return Date must be after Borrow Date !"
                });
            }

            this.Book = await this.service.GetBookByIdAsync(id);

            await this.bookBorrowService.AddBookBorrowAsync(this.Book, int.Parse(this.Borrower), borrowDate,
                returnDate);


            return this.RedirectToPage("/Index");
        }
    }
}