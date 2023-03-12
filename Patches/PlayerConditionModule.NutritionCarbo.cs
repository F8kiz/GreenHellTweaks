using GreenHellTweaks.Serializable;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionCarbo), MethodType.Setter)]
    internal class PlayerConditionModuleNutritionCarbo
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.NutritionCarbohydratesConsumptionThreshold > 0 && __instance.m_NutritionCarbo - value > config.NutritionCarbohydratesConsumptionThreshold)
                value = __instance.m_NutritionCarbo - config.NutritionCarbohydratesConsumptionThreshold;
        }
    }
}
