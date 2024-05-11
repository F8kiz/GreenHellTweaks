using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class PlayerMovementConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1f)]
        public float WalkSpeed { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float BackwardWalkSpeed { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float RunSpeed { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float DuckSpeedMultiplier { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float MaxOverloadSpeedMultiplier { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float MaxSwimSpeed { get; set; } = -1f;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
