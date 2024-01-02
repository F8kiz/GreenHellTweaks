using GHTweaks.Models;
using System.Xml.Serialization;

namespace GHTweaks.Configuration
{
    public class PocketGridConfig
    {
        public GridSize BackpackGridSize { get; set; } = new GridSize(5,10);

        public GridSize StorageGridSize { get; set; } = new GridSize(5, 10);

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
    }
}
