using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class TODTimeConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1f)]
        public float DayLengthInMinutes { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float NightLengthInMinutes { get; set; } = -1f;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
