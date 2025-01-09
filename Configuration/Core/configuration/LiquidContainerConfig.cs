using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class LiquidContainerConfig : PatchConfigBase, IPatchConfig
    {
        /// <summary>
        /// Default value 10f
        /// </summary>
        [PropertyInfo(-1f)]
        public float CoconutBowlCapacity { get; set; } = -1;

        /// <summary>
        /// Default value 40f
        /// </summary>
        [PropertyInfo(-1f)]
        public float CoconutBidonCapacity { get; set; } = -1;

        /// <summary>
        /// Default value 20f
        /// </summary>
        [PropertyInfo(-1f)]
        public float CoconutCapacity { get; set; } = -1;

        /// <summary>
        /// Default value 100f
        /// </summary>
        [PropertyInfo(-1f)]
        public float BidonCapacity { get; set; } = -1;

        /// <summary>
        /// Default value 30f
        /// </summary>
        [PropertyInfo(-1f)]
        public float PotCapacity { get; set; } = -1;

        /// <summary>
        /// Default value 100f
        /// </summary>
        public float ClayBidonCapacity { get; set; } = -1;

        /// <summary>
        /// Default value 30f
        /// </summary>
        public float ClayBowlBigCapacity { get; set; } = -1;

        /// <summary>
        /// Default value 10f
        /// </summary>
        public float ClayBowlSmallCapacity { get; set; } = -1;


        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
