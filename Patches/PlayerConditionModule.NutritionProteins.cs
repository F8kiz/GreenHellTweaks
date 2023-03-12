using GreenHellTweaks.Serializable;

using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionProteins), MethodType.Setter)]
    internal class PlayerConditionModuleNutritionProteins
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.NutritionProteinsConsumptionThreshold > 0 && __instance.m_NutritionProteins - value > config.NutritionProteinsConsumptionThreshold)
                value = __instance.m_NutritionProteins - config.NutritionProteinsConsumptionThreshold;
        }
    }
}
