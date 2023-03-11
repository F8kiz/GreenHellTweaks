using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(FoodInfo), "m_SpoilTime", MethodType.Setter)]
    internal class FoodInfoSpoilTime
    {
        static bool Prefix(ref float value)
        {
            if (Mod.Instance.Config.FoodInfoConfig.SpoilTime > 0 && value > 0)
                value = Mod.Instance.Config.FoodInfoConfig.SpoilTime;
            return true;
        }
    }
}
