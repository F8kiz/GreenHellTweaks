using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(ConstructionGhost), "CanBeAttachedToSlotBelow")]
    internal class ConstructionGhostCanBeAttachedToSlotBelow
    {
        static bool Prefix(ref bool __result)
        {
            if (!Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled)
                return true;

            if (!Mod.Instance.Config.ConstructionConfig.CanBeAttachedToSlotBelow)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
