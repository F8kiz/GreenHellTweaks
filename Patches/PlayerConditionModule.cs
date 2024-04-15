using GHTweaks.Configuration;

using HarmonyLib;

using System;
using System.Collections.Generic;
using UnityEngine;
#if DEBUG
using System.Reflection;
#endif

namespace GHTweaks.Patches
{
    // Base class for time based setter
    internal class TimeBasedConsumptionBase
    {
        private float lastConsumption = 0;

        public bool Update(float consumptionDelay, float currentValue, ref float newValue)
        {
            if (newValue >= 100 || newValue >= currentValue || consumptionDelay <= 0)
                return false;

            float elapsedMinutes = GetElapsedMinitues(lastConsumption);
            if (elapsedMinutes >= consumptionDelay)
            {
                lastConsumption = MainLevel.Instance.GetCurrentTimeMinutes();
                newValue = currentValue;
                return true;
            }
            return false;
        }

        private static float GetElapsedMinitues(float lastConsumption)
        {
            float currentTimeMinutes = MainLevel.Instance.GetCurrentTimeMinutes();
            float min = Math.Min(currentTimeMinutes, lastConsumption);
            float max = Math.Max(currentTimeMinutes, lastConsumption);

            return (max - min);
        }
    }

    // Hydration consumption
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_Hydration), MethodType.Setter)]
    internal sealed class PlayerConditionalModuleHydration : TimeBasedConsumptionBase
    {
        static PlayerConditionalModuleHydration baseInstance;

        private PlayerConditionalModuleHydration() { }

        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (baseInstance == null)
                baseInstance = new PlayerConditionalModuleHydration();

            if (baseInstance.Update(Mod.Instance.Config.PlayerConditionModuleConfig.HydrationConsumptionDelay, __instance.m_Hydration, ref value))
            {
#if DEBUG
                Mod.Instance.WriteLog($"PlayerConditionalModuleHydration.Setter: CurrentValue: {__instance.m_Hydration}, NewValue: {value}");
#endif
            }
        }
    }

    // NutritionCarbo consumption
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionCarbo), MethodType.Setter)]
    internal sealed class PlayerConditionalModuleNutritionCarbo : TimeBasedConsumptionBase
    {
        static PlayerConditionalModuleNutritionCarbo baseInstance;

        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (baseInstance == null)
                baseInstance = new PlayerConditionalModuleNutritionCarbo();

            if (baseInstance.Update(Mod.Instance.Config.PlayerConditionModuleConfig.NutritionCarboConsumptionDelay, __instance.m_NutritionCarbo, ref value))
            {
#if DEBUG
                Mod.Instance.WriteLog($"PlayerConditionalModuleNutritionCarbo.Setter: CurrentValue: {__instance.m_NutritionCarbo}, NewValue: {value}");
#endif
            }
        }
    }

    // NutritionFat consumption
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionFat), MethodType.Setter)]
    internal sealed class PlayerConditionalModuleNutritionFat : TimeBasedConsumptionBase
    {
        static PlayerConditionalModuleNutritionFat baseInstance;

        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (baseInstance == null)
                baseInstance = new PlayerConditionalModuleNutritionFat();

            if (baseInstance.Update(Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionDelay, __instance.m_NutritionFat, ref value))
            {
#if DEBUG
                Mod.Instance.WriteLog($"PlayerConditionalModuleNutritionFat.Setter: CurrentValue: {__instance.m_NutritionFat}, NewValue: {value}");
#endif
            }
        }
    }

    // NutritionProteins consumption
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionProteins), MethodType.Setter)]
    internal sealed class PlayerConditionalModuleNutritionProteins : TimeBasedConsumptionBase
    {
        static PlayerConditionalModuleNutritionProteins baseInstance;

        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (baseInstance == null)
                baseInstance = new PlayerConditionalModuleNutritionProteins();

            if (baseInstance.Update(Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionDelay, __instance.m_NutritionProteins, ref value))
            {
#if DEBUG
                Mod.Instance.WriteLog($"PlayerConditionalModuleNutritionProteins.Setter: CurrentValue: {__instance.m_NutritionProteins}, NewValue: {value}");
#endif
            }
        }
    }

    // Stamina consumption
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_Stamina), MethodType.Setter)]
    internal sealed class PlayerConditionalModuleStamina : TimeBasedConsumptionBase
    {
        static PlayerConditionalModuleStamina baseInstance;

        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (baseInstance == null)
                baseInstance = new PlayerConditionalModuleStamina();

            if (baseInstance.Update(Mod.Instance.Config.PlayerConditionModuleConfig.StaminaConsumptionDelay, __instance.m_Stamina, ref value))
            {
#if DEBUG
                Mod.Instance.WriteLog($"PlayerConditionalModuleStamina.Setter: CurrentValue: {__instance.m_Stamina}, NewValue: {value}");
#endif
            }
        }
    }

    // Energy consumption
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_Energy), MethodType.Setter)]
    internal sealed class PlayerConditionalModuleEnergy : TimeBasedConsumptionBase
    {
        static PlayerConditionalModuleEnergy baseInstance;

        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            // Updates per second: 200 - 268
            // Max deceased: 0.007499695
            // Min deceased: 0.0005264282

            if (baseInstance == null)
                baseInstance = new PlayerConditionalModuleEnergy();

            if (baseInstance.Update(Mod.Instance.Config.PlayerConditionModuleConfig.EnergyConsumptionDelay, __instance.m_Energy, ref value))
            {
#if DEBUG
                Mod.Instance.WriteLog($"PlayerConditionalModuleEnergy.Setter: CurrentValue: {__instance.m_Energy}, NewValue: {value}");
#endif
            }
        }
    }

    // Player HP
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), "m_HP", MethodType.Setter)]
    internal class PlayerConditionModuleHP
    {
        static void Prefix(ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.MinHealthPoints > 0 && value < config.MinHealthPoints)
                value = config.MinHealthPoints;
        }
    }


    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.Initialize))]
    internal class PlayerConditionModuleInitialize
    {
        static void Postfix(PlayerConditionModule __instance) => UpdateFields(__instance);

        public static void UpdateFields(PlayerConditionModule instance)
        {
            if (instance == null)
            {
                Mod.Instance.WriteLog("PlayerConditionModule.Initialize Unable to update fields, got no PlayerConditionModule instance!", LogType.Error);
                return;
            }

            var fieldsDictionary = new Dictionary<string, float>
            {
                { "m_MaxStamina", Mod.Instance.Config.PlayerConditionModuleConfig.MaxStamina }, // Default value: 100f
				{ "m_StaminaRenerationDelay", Mod.Instance.Config.PlayerConditionModuleConfig.StaminaRegenerationDelay }, // Default value: 2f
				{ "m_MaxNutritionFat", Mod.Instance.Config.PlayerConditionModuleConfig.MaxNutritionFat }, // Default value: 100f
				{ "m_MaxNutritionCarbo", Mod.Instance.Config.PlayerConditionModuleConfig.MaxNutritionCarbo }, // Default value: 100f
				{ "m_MaxNutritionProteins", Mod.Instance.Config.PlayerConditionModuleConfig.MaxNutritionProteins }, // Default value: 100f
				{ "m_MaxHydration", Mod.Instance.Config.PlayerConditionModuleConfig.MaxHydration }, // Default value: 100f
				{ "m_MaxDirtiness", Mod.Instance.Config.PlayerConditionModuleConfig.MaxDirtiness }, // Default value: 100f
				{ "m_StaminaDepletedLevel", Mod.Instance.Config.PlayerConditionModuleConfig.StaminaDepletedLevel }, // Default value: 5f
				{ "m_LowStaminaLevel", Mod.Instance.Config.PlayerConditionModuleConfig.LowStaminaLevel }, // Default value: 10f
				{ "m_LowStaminaRecoveryLevel", Mod.Instance.Config.PlayerConditionModuleConfig.LowStaminaRecoveryLevel }, // Default value: 20f
				{ "m_MaxOxygen", Mod.Instance.Config.PlayerConditionModuleConfig.MaxOxygen }, // Default value: 100f (Public field)
				{ "m_StaminaConsumptionWalkPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.StaminaConsumptionWalkPerSecond }, // Default value: ?
				{ "m_StaminaConsumptionRunPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.StaminaConsumptionRunPerSecond }, // Default value: ?
				{ "m_StaminaConsumptionDepletedPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.StaminaConsumptionDepletedPerSecond }, // Default value: ?
				{ "m_StaminaRegenerationPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.StaminaRegenerationPerSecond }, // Default value: ?
				{ "m_NutritionCarbohydratesConsumptionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionCarbohydratesConsumptionPerSecond }, // Default value: 1f
				{ "m_NutritionFatConsumptionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionPerSecond }, // Default value: 1f
				{ "m_NutritionProteinsConsumptionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionPerSecond }, // Default value: 1f
				{ "m_NutritionFatConsumptionMulNoCarbs", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionMultiplierNoCarbs }, // Default value: 1f
				{ "m_NutritionProteinsConsumptionMulNoCarbs", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionMultiplierNoCarbs }, // Default value: 1f
				{ "m_NutritionCarbohydratesConsumptionRunMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionCarbohydratesConsumptionRunMultiplier }, // Default value: 1f
				{ "m_NutritionFatConsumptionRunMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionRunMultiplier }, // Default value: 1f
				{ "m_NutritionProteinsConsumptionRunMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionRunMultiplier }, // Default value: 1f
				{ "m_NutritionCarbohydratesConsumptionActionMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionCarbohydratesConsumptionActionMultiplier }, // Default value: 1f
				{ "m_NutritionFatConsumptionActionMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionActionMultiplier }, // Default value: 2f
				{ "m_NutritionProteinsConsumptionActionMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionActionMultiplier }, // Default value: 3f
				{ "m_NutritionCarbohydratesConsumptionWeightNormalMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionCarbohydratesConsumptionWeightNormalMultiplier }, // Default value: 1f
				{ "m_NutritionFatConsumptionWeightNormalMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionWeightNormalMultiplier }, // Default value: 1f
				{ "m_NutritionProteinsConsumptionWeightNormalMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionWeightNormalMultiplier }, // Default value: 1f
				{ "m_NutritionCarbohydratesConsumptionWeightOverloadMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionCarbohydratesConsumptionWeightOverloadMultiplier }, // Default value: 1.5f
				{ "m_NutritionFatConsumptionWeightOverloadMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionWeightOverloadMultiplier }, // Default value: 1.5f
				{ "m_NutritionProteinsConsumptionWeightOverloadMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionWeightOverloadMultiplier }, // Default value: 1.5f
				{ "m_NutritionCarbohydratesConsumptionWeightCriticalMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionCarbohydratesConsumptionWeightCriticalMultiplier }, // Default value: 1.8f
				{ "m_NutritionFatConsumptionWeightCriticalMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionFatConsumptionWeightCriticalMultiplier }, // Default value: 1.8f
				{ "m_NutritionProteinsConsumptionWeightCriticalMul", Mod.Instance.Config.PlayerConditionModuleConfig.NutritionProteinsConsumptionWeightCriticalMultiplier }, // Default value: 1.8f
				{ "m_HydrationConsumptionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.HydrationConsumptionPerSecond }, // Default value: 0.5f
				{ "m_HydrationConsumptionRunMul", Mod.Instance.Config.PlayerConditionModuleConfig.HydrationConsumptionRunMultiplier }, // Default value: 0.5f
				{ "m_HydrationConsumptionDuringFeverPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.HydrationConsumptionDuringFeverPerSecond }, // Default value: 0.5f
				{ "m_OxygenConsumptionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.OxygenConsumptionPerSecond }, // Default value: 1f
				{ "m_EnergyConsumptionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.EnergyConsumptionPerSecond }, // Default value: 0.1f
				{ "m_EnergyConsumptionPerSecondNoNutrition", Mod.Instance.Config.PlayerConditionModuleConfig.EnergyConsumptionPerSecondNoNutrition }, // Default value: 0.1f
				{ "m_EnergyConsumptionPerSecondFever", Mod.Instance.Config.PlayerConditionModuleConfig.EnergyConsumptionPerSecondFever }, // Default value: 0.1f
				{ "m_EnergyConsumptionPerSecondFoodPoison", Mod.Instance.Config.PlayerConditionModuleConfig.EnergyConsumptionPerSecondFoodPoison }, // Default value: 0.1f
				{ "m_HealthLossPerSecondNoNutrition", Mod.Instance.Config.PlayerConditionModuleConfig.HealthLossPerSecondNoNutrition }, // Default value: 0.05f
				{ "m_HealthLossPerSecondNoHydration", Mod.Instance.Config.PlayerConditionModuleConfig.HealthLossPerSecondNoHydration }, // Default value: 0.05f
				{ "m_HealthRecoveryPerDayEasyMode", Mod.Instance.Config.PlayerConditionModuleConfig.HealthRecoveryPerDayEasyMode }, // Default value: 0.1f
				{ "m_HealthRecoveryPerDayNormalMode", Mod.Instance.Config.PlayerConditionModuleConfig.HealthRecoveryPerDayNormalMode }, // Default value: 0.1f
				{ "m_HealthRecoveryPerDayHardMode", Mod.Instance.Config.PlayerConditionModuleConfig.HealthRecoveryPerDayHardMode }, // Default value: 0.1f
				{ "m_HealthLossPerSecondNoOxygen", Mod.Instance.Config.PlayerConditionModuleConfig.HealthLossPerSecondNoOxygen }, // Default value: 10f
				{ "m_HydrationDecreaseJump", Mod.Instance.Config.PlayerConditionModuleConfig.HydrationDecreaseJump }, // Default value: 1f
				{ "m_EnergyLossDueLackOfNutritionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.EnergyLossDueLackOfNutritionPerSecond }, // Default value: 1f
				{ "m_EnergyRecoveryDueNutritionPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.EnergyRecoveryDueNutritionPerSecond }, // Default value: 1f
				{ "m_EnergyRecoveryDueHydrationPerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.EnergyRecoveryDueHydrationPerSecond }, // Default value: 1f
				{ "m_DirtinessIncreasePerSecond", Mod.Instance.Config.PlayerConditionModuleConfig.DirtinessIncreasePerSecond }, // Default value: 0.1f
				{ "m_DirtAddChoppingPlants", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddChoppingPlants }, // Default value: 0.01f
				{ "m_DirtAddPickickgUpHeavyObject", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddPickickgUpHeavyObject }, // Default value: 0.01f
				{ "m_DirtAddSleepingOnGround", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddSleepingOnGround }, // Default value: 0.01f
				{ "m_DirtAddUsingMud", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddUsingMud }, // Default value: 0.01f
				{ "m_DirtAddCombat", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddCombat }, // Default value: 0.01f
				{ "m_DirtAddLossConsciousness", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddLossConsciousness }, // Default value: 0.01f
				{ "m_DirtAddTakeAnimalDroppings", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddTakeAnimalDroppings }, // Default value: 0.01f
				{ "m_DirtAddPlow", Mod.Instance.Config.PlayerConditionModuleConfig.DirtAddPlow }, // Default value: 0.01f
			};

#if DEBUG
            Mod.Instance.WriteLog("PlayerConditionModule.Initialize Listing default values...");
            foreach (var kvp in fieldsDictionary)
            {
                FieldInfo fi = AccessTools.Field(typeof(PlayerConditionModule), kvp.Key);
                if (fi == null)
                {
                    Mod.Instance.WriteLog($" {kvp.Key}: ?");
                    continue;
                }
                var fieldValue = (float)fi.GetValue(instance);
                Mod.Instance.WriteLog($" {kvp.Key}: {fieldValue}");
            }
            Mod.Instance.WriteLog("PlayerConditionModule.Initialize default values listed...");
#endif 

            Type moduleType = typeof(PlayerConditionModule);
            foreach (var kvp in fieldsDictionary)
            {
                if (kvp.Value < 0)
                    continue;

                AccessTools.FieldRef<PlayerConditionModule, float> field = AccessTools.FieldRefAccess<PlayerConditionModule, float>(kvp.Key);
                if (field == null)
                {
                    Mod.Instance.WriteLog($"PlayerConditionModule.Initialize Failed to get field reference for {kvp.Key}.", LogType.Error);
                    continue;
                }
                field(instance) = kvp.Value;
            }

#if DEBUG
            // Check properties
            Mod.Instance.WriteLog("PlayerConditionModule.Initialize check field values...");
            //Type moduleType = typeof(PlayerConditionModule);
            foreach (var kvp in fieldsDictionary)
            {
                if (kvp.Value <= 0)
                    continue;

                FieldInfo fi = AccessTools.Field(moduleType, kvp.Key);
                if (fi == null)
                {
                    Mod.Instance.WriteLog($" Failed to get FieldInfo for {kvp.Key}");
                    continue;
                }
                var fieldValue = (float)fi.GetValue(instance);
                var logType = fieldValue == kvp.Value ? LogType.Debug : LogType.Error;
                Mod.Instance.WriteLog($" {kvp.Key}, current: {fieldValue}, expected: {kvp.Value}", logType);
            }
            Mod.Instance.WriteLog("PlayerConditionModule.Initialize values checked.");
#endif
        }
    }

    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), "UpdateDirtiness")]
    internal class PlayerConditionModuleUpdateDirtiness
    {
        static void Postfix(PlayerConditionModule __instance)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;

            if (config.DecreaseDirtinessOnSwimValue > 0 || config.DecreaseDirtinessOnDiveValue > 0)
            {
                SwimController sc = SwimController.Get();
                if (sc.IsActive())
                {
                    if (sc.GetState() == SwimState.Dive)
                        __instance.m_Dirtiness -= (config.DecreaseDirtinessOnDiveValue - 30) * Time.deltaTime;
                    else
                        __instance.m_Dirtiness -= (config.DecreaseDirtinessOnSwimValue - 10) * Time.deltaTime;
                }
            }

            if (config.DecreaseDirtinessOnRainValue > 0 && RainManager.Get().IsRain())
                __instance.m_Dirtiness -= (config.DecreaseDirtinessOnRainValue - 3f) * Time.deltaTime;

            __instance.m_Dirtiness = Mathf.Clamp(__instance.m_Dirtiness, 0f, __instance.m_MaxDirtiness);
        }
    }

}
