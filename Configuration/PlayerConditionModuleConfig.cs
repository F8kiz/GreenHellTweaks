namespace GHTweaks.Configuration
{
    public class PlayerConditionModuleConfig
    {
        public float MinHealthPoints { get; set; } = -1f;

        public int HydrationConsumptionDelay { get; set; } = -1;

        public int NutritionCarboConsumptionDelay { get; set; } = -1;

        public int NutritionFatConsumptionDelay { get; set; } = -1;

        public int NutritionProteinsConsumptionDelay { get; set; } = -1;

        public int StaminaConsumptionDelay { get; set; } = -1;

        public int EnergyConsumptionDelay { get; set; } = -1;

        public float DecreaseDirtinessOnRainValue { get; set; } = -1f;

        public float DecreaseDirtinessOnSwimValue { get; set; } = -1f;

        public float DecreaseDirtinessOnDiveValue { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxStamina is 100f.
        /// </summary>
        public float MaxStamina { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaRenerationDelay is 2f.
        /// </summary>
        public float StaminaRegenerationDelay { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxNutritionFat is 100f.
        /// </summary>
        public float MaxNutritionFat { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxNutritionCarbo is 100f.
        /// </summary>
        public float MaxNutritionCarbo { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxNutritionProteins is 100f.
        /// </summary>
        public float MaxNutritionProteins { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxHydration is 100f.
        /// </summary>
        public float MaxHydration { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxDirtiness is 100f.
        /// </summary>
        public float MaxDirtiness { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaDepletedLevel is 0.1f.
        /// </summary>
        public float StaminaDepletedLevel { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_LowStaminaLevel is 10f.
        /// </summary>
        public float LowStaminaLevel { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_LowStaminaRecoveryLevel is 20f.
        /// </summary>
        public float LowStaminaRecoveryLevel { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_MaxOxygen is 100f.
        /// </summary>
        public float MaxOxygen { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaConsumptionWalkPerSecond is -25f.
        /// </summary>
        public float StaminaConsumptionWalkPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaConsumptionRunPerSecond is 3f.
        /// </summary>
        public float StaminaConsumptionRunPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaConsumptionDepletedPerSecond is -0.1f.
        /// </summary>
        public float StaminaConsumptionDepletedPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_StaminaRegenerationPerSecond is 30f.
        /// </summary>
        public float StaminaRegenerationPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionPerSecond is 0.025f.
        /// </summary>
        public float NutritionCarbohydratesConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionPerSecond is 0.015f.
        /// </summary>
        public float NutritionFatConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionPerSecond is 0.02f.
        /// </summary>
        public float NutritionProteinsConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionMulNoCarbs is 1f.
        /// </summary>
        public float NutritionFatConsumptionMultiplierNoCarbs { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionMulNoCarbs is 1f.
        /// </summary>
        public float NutritionProteinsConsumptionMultiplierNoCarbs { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionRunMul is 1.2f.
        /// </summary>
        public float NutritionCarbohydratesConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionRunMul is 1f.
        /// </summary>
        public float NutritionFatConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionRunMul is 1f.
        /// </summary>
        public float NutritionProteinsConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionActionMul is 3f.
        /// </summary>
        public float NutritionCarbohydratesConsumptionActionMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionActionMul is 1f.
        /// </summary>
        public float NutritionFatConsumptionActionMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionActionMul is 2f.
        /// </summary>
        public float NutritionProteinsConsumptionActionMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionWeightNormalMul is 1f.
        /// </summary>
        public float NutritionCarbohydratesConsumptionWeightNormalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionWeightNormalMul is 1f.
        /// </summary>
        public float NutritionFatConsumptionWeightNormalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionWeightNormalMul is 1f.
        /// </summary>
        public float NutritionProteinsConsumptionWeightNormalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionWeightOverloadMul is 1.1f.
        /// </summary>
        public float NutritionCarbohydratesConsumptionWeightOverloadMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionWeightOverloadMul is 1.1f.
        /// </summary>
        public float NutritionFatConsumptionWeightOverloadMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionWeightOverloadMul is 1.1f.
        /// </summary>
        public float NutritionProteinsConsumptionWeightOverloadMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionCarbohydratesConsumptionWeightCriticalMul is 1.2f.
        /// </summary>
        public float NutritionCarbohydratesConsumptionWeightCriticalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionFatConsumptionWeightCriticalMul is 1.2f.
        /// </summary>
        public float NutritionFatConsumptionWeightCriticalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_NutritionProteinsConsumptionWeightCriticalMul is 1.2f.
        /// </summary>
        public float NutritionProteinsConsumptionWeightCriticalMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationConsumptionPerSecond is 0.04f.
        /// </summary>
        public float HydrationConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationConsumptionRunMul is 1.2f.
        /// </summary>
        public float HydrationConsumptionRunMultiplier { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationConsumptionRunMul is 0.3f.
        /// </summary>
        public float HydrationConsumptionDuringFeverPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_OxygenConsumptionPerSecond is 4f.
        /// </summary>
        public float OxygenConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecond is 0.05f.
        /// </summary>
        public float EnergyConsumptionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecondNoNutrition is 0.1f.
        /// </summary>
        public float EnergyConsumptionPerSecondNoNutrition { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecondFever is 0.1f.
        /// </summary>
        public float EnergyConsumptionPerSecondFever { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyConsumptionPerSecondFoodPoison is 0.1f.
        /// </summary>
        public float EnergyConsumptionPerSecondFoodPoison { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthLossPerSecondNoNutrition is 0f.
        /// </summary>
        public float HealthLossPerSecondNoNutrition { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthLossPerSecondNoHydration is 0f.
        /// </summary>
        public float HealthLossPerSecondNoHydration { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthLossPerSecondNoHydration is 2400f.
        /// </summary>
        public float HealthRecoveryPerDayEasyMode { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthRecoveryPerDayNormalMode is 1200f.
        /// </summary>
        public float HealthRecoveryPerDayNormalMode { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthRecoveryPerDayHardMode is 900f.
        /// </summary>
        public float HealthRecoveryPerDayHardMode { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HealthRecoveryPerDayHardMode is 10f.
        /// </summary>
        public float HealthLossPerSecondNoOxygen { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_HydrationDecreaseJump is 1f.
        /// </summary>
        public float HydrationDecreaseJump { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyLossDueLackOfNutritionPerSecond is 1f.
        /// </summary>
        public float EnergyLossDueLackOfNutritionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyLossDueLackOfNutritionPerSecond is 1f.
        /// </summary>
        public float EnergyRecoveryDueNutritionPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_EnergyRecoveryDueNutritionPerSecond is 1f.
        /// </summary>
        public float EnergyRecoveryDueHydrationPerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtinessIncreasePerSecond is 0.08f.
        /// </summary>
        public float DirtinessIncreasePerSecond { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddChoppingPlants is 1.5f.
        /// </summary>
        public float DirtAddChoppingPlants { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddPickickgUpHeavyObject is 0f.
        /// </summary>
        public float DirtAddPickickgUpHeavyObject { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddSleepingOnGround is 120f.
        /// </summary>
        public float DirtAddSleepingOnGround { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddUsingMud is 2f.
        /// </summary>
        public float DirtAddUsingMud { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddUsingMud is 2f.
        /// </summary>
        public float DirtAddCombat { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddLossConsciousness is 150f.
        /// </summary>
        public float DirtAddLossConsciousness { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddTakeAnimalDroppings is 50f.
        /// </summary>
        public float DirtAddTakeAnimalDroppings { get; set; } = -1f;

        /// <summary>
        /// The default in-game value for m_DirtAddPlow is 150f.
        /// </summary>
        public float DirtAddPlow { get; set; } = -1f;
    }
}