using GreenHellTweaks.Serializable;

using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_NutritionFat), MethodType.Setter)]
    internal class PlayerConditionModuleNutritionFat
    {
        static bool Prefix(PlayerConditionModule __instance, ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.NutritionFatConsumptionThreshold > 0 && __instance.m_NutritionFat - value > config.NutritionFatConsumptionThreshold)
            {
                __instance.m_NutritionFat -= config.NutritionFatConsumptionThreshold;
                return false;
            }
            return true;
        }
    }
}
