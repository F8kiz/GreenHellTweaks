using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(ItemSlotStack), nameof(ItemSlotStack.IsOccupied))]
    internal class ItemSlotStackIsOccupied
    {
        static bool Prefix(ItemSlotStack __instance, out bool __result)
        {
            __result = __instance.m_Items.Count >= __instance.m_StackDummies.Count;
            if (Mod.Instance.Config.InventoryBackpackConfig.UnlimitedItemStackSize)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
