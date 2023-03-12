using GreenHellTweaks.Serializable;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_Hydration), MethodType.Setter)]
    internal class PlayerConditionModuleHydration
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.HydrationConsumptionThreshold > 0 && __instance.m_Hydration - value > config.HydrationConsumptionThreshold)
                value = __instance.m_Hydration - config.HydrationConsumptionThreshold;
        }
    }
}
