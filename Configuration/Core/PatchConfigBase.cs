using System.Reflection;

namespace GHTweaks.Configuration.Core
{
    public class PatchConfigBase
    {
        protected bool CheckIfAtLeastOnePropertyHasNotTheDefaultValue<T>(T configuration)  where T : IPatchConfig
        {
            var propertyInfoAttributeType = typeof(PropertyInfoAttribute);
            var propertyInfos = configuration.GetType().GetProperties();
            foreach (var info in propertyInfos)
            {
                if (!(info.GetCustomAttribute(propertyInfoAttributeType) is PropertyInfoAttribute attribute))
                    continue;

                var value = info.GetValue(this);
                if (info.PropertyType != attribute.PropertyType)
                    continue; // TODO: report

                if (value != attribute.IsDisabledIfValue)
                    return true;
            }
            return false;
        }
    }
}
