namespace Movies.App.Models.BindingModels {
    using System.ComponentModel.DataAnnotations;
    using Infrastructure;

    public class AddMovieBindingModel {
        [Required (ErrorMessage = "Title is required.")]
        [MinLength (2, ErrorMessage = "{0} must be more then {1} symbols long.")]
        public string Title { get; set; }

        [Required (ErrorMessage = "Director Name is required.")]
        [MinLength (4, ErrorMessage = "{0} must be more then {1} symbols long.")]
        [MaxLength (32, ErrorMessage = "{0} must be less then {1} symbols long.")]
        [Display (Name = "Director Name")]
        public string Director { get; set; }

        public string Description { get; set; }

        [Url (ErrorMessage = "Invalid URL please try again.")]
        [Required (ErrorMessage = "Cover Image Url is required.")]
        [ValidateUrl (ErrorMessage = "Invalid URL please try again.")]
        public string CoverImageUrl { get; set; }
    }
}