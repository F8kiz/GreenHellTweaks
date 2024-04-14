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
        /// Default value: 100f
        /// </summary>
        public float MaxStamina { get; set; } = -1f;
        /// <summary>
        /// Default value: 2f
        /// </summary>
        public float StaminaRegenerationDelay { get; set; } = -1f;
        /// <summary>
        /// Default value: 100f
        /// </summary>
        public float MaxNutritionFat { get; set; } = -1f;
        /// <summary>
        /// Default value: 100f
        /// </summary>
        public float MaxNutritionCarbo { get; set; } = -1f;
        /// <summary>
        /// Default value: 100f
        /// </summary>
        public float MaxNutritionProteins { get; set; } = -1f;
        /// <summary>
        /// Default value: 100f
        /// </summary>
        public float MaxHydration { get; set; } = -1f;
        /// <summary>
        /// Default value: 100f
        /// </summary>
        public float MaxDirtiness { get; set; } = -1f;
        /// <summary>
        /// Default value: 5f
        /// </summary>
        public float StaminaDepletedLevel { get; set; } = -1f;
        /// <summary>
        /// Default value: 10f
        /// </summary>
        public float LowStaminaLevel { get; set; } = -1f;
        /// <summary>
        /// Default value: 20f
        /// </summary>
        public float LowStaminaRecoveryLevel { get; set; } = -1f;
        /// <summary>
        /// Default value: 100f (Public field)
        /// </summary>
        public float MaxOxygen { get; set; } = -1f;
        /// <summary>
        /// Default value: ?
        /// </summary>
        public float StaminaConsumptionWalkPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: ?
        /// </summary>
        public float StaminaConsumptionRunPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: ?
        /// </summary>
        public float StaminaConsumptionDepletedPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: ?
        /// </summary>
        public float StaminaRegenerationPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionCarbohydratesConsumptionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionFatConsumptionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionProteinsConsumptionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionFatConsumptionMultiplierNoCarbs { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionProteinsConsumptionMultiplierNoCarbs { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionCarbohydratesConsumptionRunMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionFatConsumptionRunMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionProteinsConsumptionRunMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionCarbohydratesConsumptionActionMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 2f
        /// </summary>
        public float NutritionFatConsumptionActionMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 3f
        /// </summary>
        public float NutritionProteinsConsumptionActionMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionCarbohydratesConsumptionWeightNormalMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionFatConsumptionWeightNormalMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float NutritionProteinsConsumptionWeightNormalMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1.5f
        /// </summary>
        public float NutritionCarbohydratesConsumptionWeightOverloadMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1.5f
        /// </summary>
        public float NutritionFatConsumptionWeightOverloadMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1.5f
        /// </summary>
        public float NutritionProteinsConsumptionWeightOverloadMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1.8f
        /// </summary>
        public float NutritionCarbohydratesConsumptionWeightCriticalMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1.8f
        /// </summary>
        public float NutritionFatConsumptionWeightCriticalMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 1.8f
        /// </summary>
        public float NutritionProteinsConsumptionWeightCriticalMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.5f
        /// </summary>
        public float HydrationConsumptionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.5f
        /// </summary>
        public float HydrationConsumptionRunMultiplier { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.5f
        /// </summary>
        public float HydrationConsumptionDuringFeverPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float OxygenConsumptionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float EnergyConsumptionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float EnergyConsumptionPerSecondNoNutrition { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float EnergyConsumptionPerSecondFever { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float EnergyConsumptionPerSecondFoodPoison { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.05f
        /// </summary>
        public float HealthLossPerSecondNoNutrition { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.05f
        /// </summary>
        public float HealthLossPerSecondNoHydration { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float HealthRecoveryPerDayEasyMode { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float HealthRecoveryPerDayNormalMode { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float HealthRecoveryPerDayHardMode { get; set; } = -1f;
        /// <summary>
        /// Default value: 10f
        /// </summary>
        public float HealthLossPerSecondNoOxygen { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float HydrationDecreaseJump { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float EnergyLossDueLackOfNutritionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float EnergyRecoveryDueNutritionPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 1f
        /// </summary>
        public float EnergyRecoveryDueHydrationPerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.1f
        /// </summary>
        public float DirtinessIncreasePerSecond { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddChoppingPlants { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddPickickgUpHeavyObject { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddSleepingOnGround { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddUsingMud { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddCombat { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddLossConsciousness { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddTakeAnimalDroppings { get; set; } = -1f;
        /// <summary>
        /// Default value: 0.01f
        /// </summary>
        public float DirtAddPlow { get; set; } = -1f;
    }
}