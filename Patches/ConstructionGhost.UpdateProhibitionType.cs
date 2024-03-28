using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(ConstructionGhost), nameof(ConstructionGhost.UpdateProhibitionType))]
    internal class ConstructionGhostUpdateProhibitionType
    {
        static bool Prefix(ConstructionGhost __instance)
        {
            if (Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled)
            {
                __instance.m_ProhibitionType = ConstructionGhost.ProhibitionType.None;
                return false;
            }
            return true;
        }
    }
}
