using System;

namespace GHTweaks.Configuration.Core
{
    internal class PropertyInfoAttribute : Attribute
    {
        public object IsDisabledIfValue { get; set; }

        public Type PropertyType { get; set; }

        public string PropertyName { get; set; }



        public PropertyInfoAttribute() { }

        public PropertyInfoAttribute(string propertyName) 
            : this(default, propertyName, default) { }

        public PropertyInfoAttribute(object isDisabledIfValueIs) 
            : this(isDisabledIfValueIs.GetType(), "", isDisabledIfValueIs) { }

        public PropertyInfoAttribute(string propertyName, object isDisabledIfValueIs) 
            : this(isDisabledIfValueIs.GetType(), propertyName, isDisabledIfValueIs) { }

        public PropertyInfoAttribute(Type propertyType, string propertyName, object isDisabledIfValueIs) 
        {
            PropertyType = propertyType;
            PropertyName = propertyName;
            IsDisabledIfValue = isDisabledIfValueIs;
        }
    }
}
