using GHTweaks.Configuration;

using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionFat), MethodType.Setter)]
    internal class PlayerConditionModuleNutritionFat
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (value == 100)
                return;

            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.NutritionFatConsumptionThreshold > 0)
            {
                if (__instance.m_NutritionFat - value > config.NutritionFatConsumptionThreshold)
                    value = __instance.m_NutritionFat - config.NutritionFatConsumptionThreshold;
            }
            else if (config.NutritionFatConsumptionMultiplier > 0)
            {
                value = __instance.m_NutritionFat - ((__instance.m_NutritionFat - value) * config.NutritionFatConsumptionMultiplier);
            }
        }
    }
}
