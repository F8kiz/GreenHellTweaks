using GreenHellTweaks.Serializable;

using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionProteins), MethodType.Setter)]
    internal class PlayerConditionModuleNutritionProteins
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (value == 100)
                return;

            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.NutritionProteinsConsumptionThreshold > 0)
            {
                if (__instance.m_NutritionProteins - value > config.NutritionProteinsConsumptionThreshold)
                    value = __instance.m_NutritionProteins - config.NutritionProteinsConsumptionThreshold;
            }
            else if (config.NutritionProteinsConsumptionMultiplier > 0)
            {
                value = __instance.m_NutritionProteins - ((__instance.m_NutritionProteins - value) * config.NutritionProteinsConsumptionMultiplier);
            }
        }
    }
}
