using HarmonyLib;


namespace GHTweaks.Patches
{
    //[HarmonyPatch(typeof(ConstructionGhost), nameof(ConstructionGhost.IsReady))]
    internal class ConstructionGhostIsReady
    {
        static bool Prefix(ref bool __result)
        {
            if (Mod.Instance.Config.ConstructionConfig.InstantBuild)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}
