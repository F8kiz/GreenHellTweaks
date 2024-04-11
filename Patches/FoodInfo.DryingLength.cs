using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(FoodInfo), "m_DryingLength", MethodType.Setter)]
    internal class FoodInfoDryingLength
    {
        static void Prefix(FoodInfo __instance, ref float value)
        {
            if (value < 1)
                return;

            if (!__instance.IsMeat())
                value = 0.25f;
        }
    }
}
