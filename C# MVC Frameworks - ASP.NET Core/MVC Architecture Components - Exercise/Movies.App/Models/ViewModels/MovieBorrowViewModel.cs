namespace Movies.App.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class MovieBorrowViewModel
    {
        public int Id { get; set; }

        [Required] public string CoverImageUrl { get; set; }

        public List<SelectListItem> Borrowers { get; set; } = new List<SelectListItem>();

        public int BorrowerId { get; set; }

        public string RentDate { get; set; }

        public string ReturnDate { get; set; }
    }
}