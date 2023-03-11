using HarmonyLib;
using System.Reflection;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(Torch), "UpdateBurning")]
    internal class TorchUpdateBurning
    {
        static bool Prefix(Torch __instance)
        {
            FieldInfo fiBurning = AccessTools.Field(typeof(Firecamp), nameof(Firecamp.m_Burning));
            bool isBurning = (bool)fiBurning.GetValue(__instance);
            if (!isBurning)
                return true;

            if (Mod.Instance.Config.TorchConfig.InfiniteBurn)
                return false;
            return true;
        }
    }
}
