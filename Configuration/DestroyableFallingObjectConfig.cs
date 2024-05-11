using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class DestroyableFallingObjectConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1f)]
        public float MaxTimeToDestroy { get; set; } = -1; // Default game value: 3f

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
