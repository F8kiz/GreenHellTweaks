using HarmonyLib;
using System.Reflection;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(Torch), "UpdateBurning")]
    internal class TorchUpdateBurning
    {
        static bool Prefix(Torch __instance)
        {
            if (Mod.Instance.Config.TorchConfig.InfiniteBurn)
            {
                FieldInfo fiBurning = AccessTools.Field(typeof(Firecamp), nameof(Firecamp.m_Burning));
                bool isBurning = (bool)fiBurning.GetValue(__instance);
                if (!isBurning)
                    return true;

                return false;
            }

            return true;
        }
    }
}
