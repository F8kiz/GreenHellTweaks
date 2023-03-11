using GreenHellTweaks.Serializable;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionCarbo), MethodType.Setter)]
    internal class PlayerConditionModuleNutritionCarbo
    {
        static bool Prefix(PlayerConditionModule __instance, ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.NutritionCarbohydratesConsumptionThreshold > 0 && __instance.m_NutritionCarbo - value > config.NutritionCarbohydratesConsumptionThreshold)
            {
                __instance.m_NutritionCarbo -= config.NutritionCarbohydratesConsumptionThreshold;
                return false;
            }
            return true;
        }
    }
}
