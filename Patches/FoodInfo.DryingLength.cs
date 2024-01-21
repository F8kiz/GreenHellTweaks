using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(FoodInfo), "m_DryingLength", MethodType.Setter)]
    internal class FoodInfoDryingLength
    {
        static void Prefix(FoodInfo __instance, ref float value)
        {
            if (value < 1)
                return;

            Mod.Instance.WriteLog($"FoodInfo {__instance.m_ID}.m_DryingLength: {value}");
            if (!__instance.IsMeat())
                value = 0.25f;
        }
    }
}
