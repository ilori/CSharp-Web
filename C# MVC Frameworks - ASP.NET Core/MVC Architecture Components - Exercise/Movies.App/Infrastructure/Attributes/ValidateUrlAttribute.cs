namespace Movies.App.Infrastructure
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class ValidateUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string url = (string) value;

            if (url == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^(https?:\/\/.*\.(?:png|jpg))$");

            return regex.IsMatch(url);
        }
    }
}