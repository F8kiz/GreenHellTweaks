using GHTweaks.Configuration.Core;
using GHTweaks.Models;
using System.Xml.Serialization;

namespace GHTweaks.Configuration
{
    public class PocketGridConfig : IPatchConfig
    {
        public GridSize BackpackMainGridSize { get; set; } = new GridSize(15,10);

        public GridSize BackpackFrontGridSize { get; set; } = new GridSize(10,10);

        public GridSize StorageGridSize { get; set; } = new GridSize(15, 10);

        public float ItemScale 
        {
            get => itemScale;
            set
            {
                if (value < MinItemScale || value > 100)
                    return;

                itemScale = value;
            }
        }
        private float itemScale = 100f;

        [XmlIgnore]
        public readonly float MaxItemScale = 100f;

        [XmlIgnore]
        public readonly float MinItemScale = 10f;

        public bool HasAtLeastOneEnabledPatch
        {
            get
            {
                if (BackpackMainGridSize == new GridSize(15, 10))
                    return true;

                if (BackpackFrontGridSize == new GridSize(10, 10))
                    return true;

                if (StorageGridSize == new GridSize(15, 10))
                    return true;

                if (itemScale  != 100f)
                    return true;

                return false;
            }
        }
    }
}
