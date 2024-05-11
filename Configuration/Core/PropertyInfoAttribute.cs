using System;

namespace GHTweaks.Configuration.Core
{
    internal class PropertyInfoAttribute : Attribute
    {
        public object IsDisabledIfValue { get; set; }

        public Type PropertyType { get; set; }


        public PropertyInfoAttribute() { }

        public PropertyInfoAttribute(object isDisabledIfValueIs) : this(isDisabledIfValueIs.GetType(), isDisabledIfValueIs) { }

        public PropertyInfoAttribute(Type propertyType, object isDisabledIfValueIs) 
        {
            PropertyType = propertyType;
            IsDisabledIfValue = isDisabledIfValueIs;
        }
    }
}
