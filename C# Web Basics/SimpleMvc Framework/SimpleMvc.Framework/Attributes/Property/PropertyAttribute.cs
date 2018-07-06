namespace SimpleMvc.Framework.Attributes.Property
{
    using System;

    public abstract class PropertyAttribute : Attribute
    {
        protected PropertyAttribute()
        {
        }

        protected PropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }


        protected string PropertyName { get; }

        public abstract bool IsValid(object value);

        public abstract string ErrorMessage { get; }
    }
}