using GHTweaks.Configuration;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_Hydration), MethodType.Setter)]
    internal class PlayerConditionModuleHydration
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            if (value == 100)
                return;

            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.HydrationConsumptionThreshold > 0)
            {
                if (__instance.m_Hydration - value > config.HydrationConsumptionThreshold)
                    value = __instance.m_Hydration - config.HydrationConsumptionThreshold;
            }
            else if (config.HydrationConsumptionMultiplier > 0)
            {
                value = __instance.m_Hydration - ((__instance.m_Hydration - value) * config.HydrationConsumptionMultiplier);
            }
        }
    }
}
