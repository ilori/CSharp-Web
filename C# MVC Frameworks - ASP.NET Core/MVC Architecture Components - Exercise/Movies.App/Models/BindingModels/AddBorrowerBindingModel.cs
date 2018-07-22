namespace Movies.App.Models.BindingModels
{

    using System.ComponentModel.DataAnnotations;

    public class AddBorrowerBindingModel
    {

        [Required]
        [MinLength(2,ErrorMessage = "{0} must be atleast {1} symbols long.")]
        [MaxLength(15,ErrorMessage = "{0} must be less than {1} symbols.")]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Address { get; set; }

    }

}