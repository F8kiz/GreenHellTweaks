using GHTweaks.Utilities;
using HarmonyLib;


namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.MenuDebug)]
    [HarmonyPatch(typeof(ItemsManager), "UpdateDebug")]
    internal class ItemsManagerUpdateDebug
    {
        static void Postfix()
        {
            Mod.Instance.OnUpdate();
            HighlightVicinityItems.OnUpdate();
        }
    }
}
