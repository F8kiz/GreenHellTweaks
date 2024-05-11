using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class SkillConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(0f)]
        public float SkillProgressMultiplier { get; set; } = 0f;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
