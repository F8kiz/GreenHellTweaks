using System.Collections.Generic;

namespace GHTweaks.Configuration
{
    public class AISoundModuleConfig
    {
        public float MouseMaxDistance 
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref mouseMaxDistance, value);
        }
        private float mouseMaxDistance = -1;

        public float PeccaryMaxDistance
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref peccaryMaxDistance, value);
        }
        private float peccaryMaxDistance = -1;

        public float CapybaraMaxDistance
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref capybaraMaxDistance, value);
        }
        private float capybaraMaxDistance = -1;

        public float TapirMaxDistance
        {
            get => mouseMaxDistance;
            set => SetBackingField(ref tapirMaxDistance, value);
        }
        private float tapirMaxDistance = -1;


        private void SetBackingField(ref float field, float value)
        {
            if (value >= 0 && value <= 100)
                field = value;
        }
    }
}
