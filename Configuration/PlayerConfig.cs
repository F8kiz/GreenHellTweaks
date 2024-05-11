using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class PlayerConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1)]
        public float MinFallingHeightToDealDamage { get; set; } = -1;

        [PropertyInfo(false)]
        public bool SteadyAim { get; set; } = false;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
