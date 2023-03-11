using System.Xml.Serialization;

namespace GreenHellTweaks.Serializable
{
    public class ConstructionConfig
    {
        [XmlIgnore]
        public bool CanBeAttachedToSlotBelow { get; set; }

        public bool PlaceEveryWhereEnabled { get; set; }

        public bool InstantBuild { get; set; }

        public bool DestroyConstructionWithoutItems { get; set; }

        public bool OneShotConstructions { get; set; }


        public ConstructionConfig()
        {
            CanBeAttachedToSlotBelow = true;
            PlaceEveryWhereEnabled = true;
        }
    }
}
