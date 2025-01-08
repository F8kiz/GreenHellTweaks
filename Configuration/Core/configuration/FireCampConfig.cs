using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class FireCampConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(false)]
        public bool EndlessFire { get; set; }

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
