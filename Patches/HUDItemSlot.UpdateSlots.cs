using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.InventoryBackpack)]
    [HarmonyPatch(typeof(HUDItemSlot), "UpdateSlots")]
    internal class HUDItemSlotUpdateSlots
    {
        static void Prefix(SlotData data, out SlotData __state)
        {
            __state = data;
        }

        static void Postfix(HUDItemSlot __instance, ref SlotData __state)
        {
            if (Mod.Instance.Config.InventoryBackpackConfig.UnlimitedItemStackSize)
            {
                if (Inventory3DManager.Get().m_SelectedSlot == __state.slot || __state.slot.m_InventoryStackSlot)
                    __state.icon.color = __instance.m_SelectedColor;
            }
        }
    }
}
