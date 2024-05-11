using GHTweaks.Configuration.Core;

namespace GHTweaks.Configuration
{
    public class PlayerConditionModuleConfig : PatchConfigBase, IPatchConfig
    {
        [PropertyInfo(-1f)]
        public float MinHealthPoints { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public int HydrationConsumptionDelay { get; set; } = -1;

        [PropertyInfo(-1f)]
        public int NutritionCarboConsumptionDelay { get; set; } = -1;

        [PropertyInfo(-1f)]
        public int NutritionFatConsumptionDelay { get; set; } = -1;

        [PropertyInfo(-1f)]
        public int NutritionProteinsConsumptionDelay { get; set; } = -1;

        [PropertyInfo(-1f)]
        public int StaminaConsumptionDelay { get; set; } = -1;

        [PropertyInfo(-1f)]
        public int EnergyConsumptionDelay { get; set; } = -1;

        [PropertyInfo(-1f)]
        public float DecreaseDirtinessOnRainValue { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float DecreaseDirtinessOnSwimValue { get; set; } = -1f;

        [PropertyInfo(-1f)]
        public float DecreaseDirtinessOnDiveValue { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxStamina is 100f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxStamina { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaRenerationDelay is 2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float StaminaRegenerationDelay { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxNutritionFat is 100f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxNutritionFat { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxNutritionCarbo is 100f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxNutritionCarbo { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxNutritionProteins is 100f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxNutritionProteins { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxHydration is 100f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxHydration { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxDirtiness is 100f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxDirtiness { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaDepletedLevel is 0.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float StaminaDepletedLevel { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_LowStaminaLevel is 10f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float LowStaminaLevel { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_LowStaminaRecoveryLevel is 20f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float LowStaminaRecoveryLevel { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxOxygen is 100f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float MaxOxygen { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaConsumptionWalkPerSecond is -25f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float StaminaConsumptionWalkPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaConsumptionRunPerSecond is 3f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float StaminaConsumptionRunPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaConsumptionDepletedPerSecond is -0.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float StaminaConsumptionDepletedPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaRegenerationPerSecond is 30f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float StaminaRegenerationPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionPerSecond is 0.025f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionCarbohydratesConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionPerSecond is 0.015f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionFatConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionPerSecond is 0.02f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionProteinsConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionMulNoCarbs is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionFatConsumptionMultiplierNoCarbs { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionMulNoCarbs is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionProteinsConsumptionMultiplierNoCarbs { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionRunMul is 1.2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionCarbohydratesConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionRunMul is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionFatConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionRunMul is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionProteinsConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionActionMul is 3f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionCarbohydratesConsumptionActionMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionActionMul is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionFatConsumptionActionMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionActionMul is 2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionProteinsConsumptionActionMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionWeightNormalMul is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionCarbohydratesConsumptionWeightNormalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionWeightNormalMul is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionFatConsumptionWeightNormalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionWeightNormalMul is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionProteinsConsumptionWeightNormalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionWeightOverloadMul is 1.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionCarbohydratesConsumptionWeightOverloadMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionWeightOverloadMul is 1.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionFatConsumptionWeightOverloadMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionWeightOverloadMul is 1.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionProteinsConsumptionWeightOverloadMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionWeightCriticalMul is 1.2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionCarbohydratesConsumptionWeightCriticalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionWeightCriticalMul is 1.2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionFatConsumptionWeightCriticalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionWeightCriticalMul is 1.2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float NutritionProteinsConsumptionWeightCriticalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationConsumptionPerSecond is 0.04f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HydrationConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationConsumptionRunMul is 1.2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HydrationConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationConsumptionRunMul is 0.3f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HydrationConsumptionDuringFeverPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_OxygenConsumptionPerSecond is 4f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float OxygenConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecond is 0.05f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float EnergyConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecondNoNutrition is 0.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float EnergyConsumptionPerSecondNoNutrition { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecondFever is 0.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float EnergyConsumptionPerSecondFever { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecondFoodPoison is 0.1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float EnergyConsumptionPerSecondFoodPoison { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthLossPerSecondNoNutrition is 0f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HealthLossPerSecondNoNutrition { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthLossPerSecondNoHydration is 0f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HealthLossPerSecondNoHydration { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthLossPerSecondNoHydration is 2400f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HealthRecoveryPerDayEasyMode { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthRecoveryPerDayNormalMode is 1200f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HealthRecoveryPerDayNormalMode { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthRecoveryPerDayHardMode is 900f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HealthRecoveryPerDayHardMode { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthRecoveryPerDayHardMode is 10f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HealthLossPerSecondNoOxygen { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationDecreaseJump is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float HydrationDecreaseJump { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyLossDueLackOfNutritionPerSecond is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float EnergyLossDueLackOfNutritionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyLossDueLackOfNutritionPerSecond is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float EnergyRecoveryDueNutritionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyRecoveryDueNutritionPerSecond is 1f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float EnergyRecoveryDueHydrationPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtinessIncreasePerSecond is 0.08f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtinessIncreasePerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddChoppingPlants is 1.5f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddChoppingPlants { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddPickickgUpHeavyObject is 0f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddPickickgUpHeavyObject { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddSleepingOnGround is 120f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddSleepingOnGround { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddUsingMud is 2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddUsingMud { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddUsingMud is 2f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddCombat { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddLossConsciousness is 150f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddLossConsciousness { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddTakeAnimalDroppings is 50f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddTakeAnimalDroppings { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddPlow is 150f.
        /// </summary>
        [PropertyInfo(-1f)]
        public float DirtAddPlow { get; set; } = -1f;

        public bool HasAtLeastOneEnabledPatch => CheckIfAtLeastOnePropertyHasNotTheDefaultValue(this);
    }
}