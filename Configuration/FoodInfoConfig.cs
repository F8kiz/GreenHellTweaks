using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class FoodInfoConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1f)]
        public float SpoilTime { get; set; } = -1f;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
