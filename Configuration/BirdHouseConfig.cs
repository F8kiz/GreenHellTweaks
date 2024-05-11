using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class BirdHouseConfig : PatchConfigBase, IPatchConfig
    {
        /// <summary>
        /// Maximum number of birds in the nest.
        /// Default value is 2
        /// </summary>
        [PropertyInfo(0)]
        public int MaxBirdsCount { get; set; } = -1;

        /// <summary>
        /// Time after building bird_house after which birds will appear [hours]
        /// Default value is 2f
        /// </summary>
        [PropertyInfo(-1f)]
        public float SpawnBirdsDelay { get; set; } = -1f;

        /// <summary>
        /// Time after the birds appear, after which they 'domesticate' and start nesting [hours]
        /// Default value is 6f
        /// </summary>
        [PropertyInfo(-1f)]
        public float DomesticationTime { get; set; } = -1f;

        /// <summary>
        /// Time after killing a bird before another one appears [hours]
        /// Default value is 6f
        /// </summary>
        [PropertyInfo(-1f)]
        public float BirdKillPenaltyTime { get; set; } = -1f;

        /// <summary>
        /// Get or set the LandingSpotController.m_LandingSpotController value.
        /// Default value is 20f
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxBirdDistanceMultiplier { get; set; } = -1f;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}
