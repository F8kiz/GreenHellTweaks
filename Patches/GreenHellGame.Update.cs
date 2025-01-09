using GHTweaks.Utilities;
using HarmonyLib;


namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.GreenHellGameUpdate)]
    [HarmonyPatch(typeof(GreenHellGame), "Update")]
    internal class GreenHellGameUpdate
    {
        static void Postfix()
        {
            Mod.Instance.OnUpdate();
            HighlightVicinityItems.OnUpdate();
        }
    }
}
