namespace GHTweaks.Configuration
{
    public class AISoundModuleConfig
    {
        public float MouseMaxDistance 
        {
            get => mouseMaxDistance;
            set
            {
                if (value <= 100)
                    mouseMaxDistance = value;
            }
        }
        private float mouseMaxDistance = -1;
    }
}
