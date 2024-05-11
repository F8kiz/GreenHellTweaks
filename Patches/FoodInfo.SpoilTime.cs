using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.FoodInfo)]
    [HarmonyPatch(typeof(FoodInfo), "m_SpoilTime", MethodType.Setter)]
    internal class FoodInfoSpoilTime
    {
        static void Prefix(FoodInfo __instance, ref float value)
        {
            if (value < 1)
                return;
            
            if (__instance.IsMeat() && Mod.Instance.Config.FoodInfoConfig.SpoilTime > 0)
                value = Mod.Instance.Config.FoodInfoConfig.SpoilTime;
        }
    }
}
