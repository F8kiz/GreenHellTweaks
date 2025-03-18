using GHTweaks.Configuration.Core;

using System.Xml.Serialization;

namespace GHTweaks.Configuration
{
    public class ConstructionConfig : PatchConfigBase, IPatchConfig
    {
        [XmlIgnore]
        public bool CanBeAttachedToSlotBelow { get; set; } = false;

        [PropertyInfo(false)]
        public bool PlaceEveryWhereEnabled { get; set; } = true;

        [PropertyInfo(false)]
        public bool InstantBuild { get; set; } = false;

        [PropertyInfo(false)]
        public bool DestroyConstructionWithoutItems { get; set; } = false;

        [PropertyInfo(false)]
        public bool OneShotConstructions { get; set; } = false;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
