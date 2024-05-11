using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class InventoryBackpackConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(50f)]
        public float MaxWeight { get; set; } = 50f;

        [PropertyInfo(false)]
        public bool UnlimitedItemStackSize { get; set; }

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
