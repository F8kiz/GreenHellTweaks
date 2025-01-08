using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class ItemInfoConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(false)]
        public bool UnbreakableWeapons { get; set; }

        [PropertyInfo(false)]
        public bool UnbreakableArmor { get; set; }

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
