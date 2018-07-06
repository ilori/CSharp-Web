namespace SimpleMvc.Framework.Attributes.Property
{
    public class PasswordAttribute : PropertyAttribute
    {
        public PasswordAttribute()
        {
        }

        public PasswordAttribute(string propertyName) : base(propertyName)
        {
        }

        private const int PasswordMinLength = 6;
        private const int PasswordMaxLength = 15;

        public override bool IsValid(object value)
        {
            string password = value as string;

            return password.Length >= PasswordMinLength && password.Length <= PasswordMaxLength;
        }

        public override string ErrorMessage =>
            $"{this.PropertyName} length must be atleast {PasswordMinLength} symbols and less than {PasswordMaxLength} symbols";
    }
}