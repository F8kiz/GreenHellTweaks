using GreenHellTweaks.Serializable;
using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
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
                __instance.m_Dirtiness -= (config.DecreaseDirtinessOnRainValue -3f) * Time.deltaTime;

            __instance.m_Dirtiness = Mathf.Clamp(__instance.m_Dirtiness, 0f, __instance.m_MaxDirtiness);
        }
    }
}
