using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(ItemSlotStack), nameof(ItemSlotStack.IsOccupied))]
    internal class ItemSlotStackIsOccupied
    {
        static bool Prefix(ref bool __result)
        {
            if (Mod.Instance.Config.InventoryBackpackConfig.UnlimitedItemStackSize)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
