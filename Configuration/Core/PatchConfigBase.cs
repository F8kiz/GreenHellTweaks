﻿using System.Collections.Generic;
using System.IO;
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

        protected void SetBackingField<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;
        }

        protected void SetBackingField(ref float field, float value, float minValue, float maxValue)
        {
            if (field == value)
                return;

            if (value < minValue || value > maxValue)
                return;

            field = value;
        }
    }
}
