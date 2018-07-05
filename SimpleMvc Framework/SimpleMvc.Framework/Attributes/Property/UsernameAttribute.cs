namespace SimpleMvc.Framework.Attributes.Property
{
    public class UsernameAttribute : PropertyAttribute
    {
        public UsernameAttribute()
        {
        }

        public UsernameAttribute(string propertyName) : base(propertyName)
        {
        }

        private const int UsernameMinLength = 3;
        private const int UsernameMaxLength = 20;

        public override bool IsValid(object value)
        {
            string username = value as string;

            return username.Length >= UsernameMinLength && username.Length <= UsernameMaxLength;
        }

        public override string ErrorMessage =>
            $"{this.PropertyName} must be atleast {UsernameMinLength} symbols and less than {UsernameMaxLength} symbols";
    }
}