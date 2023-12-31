using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(ConstructionGhost), "CanBeAttachedToSlotBelow")]
    internal class ConstructionGhostCanBeAttachedToSlotBelow
    {
        static void Postfix(ref bool __result)
        {
            if (!Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled)
                return;

            if (Mod.Instance.Config.ConstructionConfig.CanBeAttachedToSlotBelow)
            {
                __result = true;
            }
        }
    }
}
