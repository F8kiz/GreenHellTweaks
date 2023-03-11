using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(InventoryBackpack), "InsertItemMain")]
    internal class InventoryBackpackInsertItemMain
    {
        static void Prefix(ref bool can_stack)
        {
            can_stack = true;
        }
    }
}
