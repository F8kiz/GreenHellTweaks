using GHTweaks.Serializable;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(TOD_Time), "UpdateTime")]
    internal class TOD_TimeUpdateTime
    {
        static void Postfix(TOD_Time __instance)
        {
            TODTimeConfig config = Mod.Instance.Config.TODTimeConfig;
            if (config.DayLengthInMinutes > 0)
                __instance.m_DayLengthInMinutes = config.DayLengthInMinutes;

            if (config.NightLengthInMinutes > 0)
                __instance.m_NightLengthInMinutes = config.NightLengthInMinutes;
        }
    }
}
