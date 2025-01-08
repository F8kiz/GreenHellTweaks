using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class TorchConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(false)]
        public bool InfiniteBurn { get; set; } = false;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
