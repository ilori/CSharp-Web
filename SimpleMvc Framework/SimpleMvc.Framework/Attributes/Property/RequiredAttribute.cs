namespace SimpleMvc.Framework.Attributes.Property
{
    public class RequiredAttribute : PropertyAttribute
    {
        public RequiredAttribute()
        {
        }

        public RequiredAttribute(string properyName) : base(properyName)
        {
        }

        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }

        public override string ErrorMessage => $"{this.PropertyName} is required";
    }
}