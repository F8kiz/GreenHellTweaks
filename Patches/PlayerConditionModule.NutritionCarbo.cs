using GreenHellTweaks.Serializable;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionCarbo), MethodType.Setter)]
    internal class PlayerConditionModuleNutritionCarbo
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (value == 100)
                return;

            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.NutritionCarbohydratesConsumptionThreshold > 0)
            {
                if (__instance.m_NutritionCarbo - value > config.NutritionCarbohydratesConsumptionThreshold)
                    value = __instance.m_NutritionCarbo - config.NutritionCarbohydratesConsumptionThreshold;
            }
            else if (config.NutritionCarbohydratesConsumptionMultiplier > 0)
            {
                value = __instance.m_NutritionCarbo - ((__instance.m_NutritionCarbo - value) * config.NutritionCarbohydratesConsumptionMultiplier);
            }
        }
    }
}
