using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(ItemSlotStack), nameof(ItemSlotStack.IsOccupied))]
    internal class ItemSlotStackIsOccupied
    {
        static bool Prefix(ItemSlotStack __instance, out bool __result)
        {
            __result = __instance.m_Items.Count >= __instance.m_StackDummies.Count;
            if (Mod.Instance.Config.InventoryBackpackConfig.UnlimitedItemStackSize)
            {
                if (__result)
                {
                    __instance.m_StackDummies.Add(new UnityEngine.GameObject());
                }
                __result = false;
                return false;
            }
            return true;
        }
    }
}
