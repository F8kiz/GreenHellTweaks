
using HarmonyLib;

namespace GHTweaks.Patches
{
    //[HarmonyPatch(typeof(Inventory3DManager), "TryAttractCarriedItemToSlot")]
    internal class Inventory3DManagerTryAttractCarriedItemToSlot
    {
        static void Postfix(Inventory3DManager __instance)
        {
            if (Mod.Instance.Config.InventoryBackpackConfig.UnlimitedItemStackSize)
            {
                if (__instance.m_SelectedSlot.IsStack())
                {
                    ItemSlotStack itemSlotStack = (ItemSlotStack)__instance.m_SelectedSlot;
                    int index = System.Math.Min(itemSlotStack.m_StackDummies.Count, itemSlotStack.m_Items.Count) - 1;
                    __instance.m_CarriedItem.transform.position = itemSlotStack.m_StackDummies[index].transform.position;
                    if (itemSlotStack.m_AdjustRotation)
                        __instance.m_CarriedItem.transform.rotation = itemSlotStack.m_StackDummies[index].transform.rotation;
                }
            }
        }
    }
}
