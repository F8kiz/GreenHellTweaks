using GHTweaks.Utilities;
using HarmonyLib;


namespace GHTweaks.Patches
{
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
