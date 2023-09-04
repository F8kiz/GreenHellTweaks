using System.Xml.Serialization;

namespace GHTweaks.Serializable
{
    public class ConstructionConfig
    {
        [XmlIgnore]
        public bool CanBeAttachedToSlotBelow { get; set; } = true;

        public bool PlaceEveryWhereEnabled { get; set; } = true;

        public bool InstantBuild { get; set; }

        public bool DestroyConstructionWithoutItems { get; set; }

        public bool OneShotConstructions { get; set; }
    }
}
