using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class PlayerMovementConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1)]
        public float WalkSpeed { get; set; } = -1f;

        [PropertyInfo(-1)]
        public float BackwardWalkSpeed { get; set; } = -1f;

        [PropertyInfo(-1)]
        public float RunSpeed { get; set; } = -1f;

        [PropertyInfo(-1)]
        public float DuckSpeedMultiplier { get; set; } = -1f;

        [PropertyInfo(-1)]
        public float MaxOverloadSpeedMultiplier { get; set; } = -1f;

        [PropertyInfo(-1)]
        public float MaxSwimSpeed { get; set; } = -1f;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
