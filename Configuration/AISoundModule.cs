﻿using GHTweaks.Configuration.Core;
using System.Reflection;

namespace GHTweaks.Configuration
{
    public class AISoundModuleConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1f)]
        public float MouseMaxDistance 
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref mouseMaxDistance, value);
        }
        private float mouseMaxDistance = -1;

        [PropertyInfo(-1f)]
        public float PeccaryMaxDistance
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref peccaryMaxDistance, value);
        }
        private float peccaryMaxDistance = -1;

        [PropertyInfo(-1f)]
        public float CapybaraMaxDistance
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref capybaraMaxDistance, value);
        }
        private float capybaraMaxDistance = -1;

        [PropertyInfo(-1f)]
        public float TapirMaxDistance
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref tapirMaxDistance, value);
        }
        private float tapirMaxDistance = -1;

        /// <summary>
        /// Returns true if at least one property has not the default value.
        /// </summary>
        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);   


        private void SetBackingField(ref float field, float value)
        {
            if (value >= 0 && value <= 100)
                field = value;
        }
    }
}
