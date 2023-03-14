using Enums;
using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(Inventory3DManager), "TryAttractCarriedItemToSlot")]
    internal class Inventory3DManagerTryAttractCarriedItemToSlot
    {
        static bool Prefix(Inventory3DManager __instance, ref bool __result)
        {
            if (Mod.Instance.Config.InventoryBackpackConfig.UnlimitedItemStackSize)
                return true;

            Vector3 b = Vector3.zero;
            ItemSlot itemSlot = null;
            float num = float.MaxValue;
            foreach (ItemSlot itemSlot2 in ItemSlot.s_ActiveItemSlots)
            {
                if (itemSlot2 && itemSlot2.CanInsertItem(__instance.m_CarriedItem) && !ReplicatedLogicalPlayer.IsSlotSelectedByOtherPlayer(itemSlot2))
                {
                    b = itemSlot2.GetScreenPoint();
                    float num2 = Vector3.Distance(Input.mousePosition, b);
                    if (num2 <= itemSlot2.m_AttrRange && num2 <= num && (itemSlot2.IsStack() || itemSlot2.m_BackpackSlot || Vector3.Distance(itemSlot2.GetCheckPosition(), Player.Get().transform.position) <= ItemSlot.s_DistToActivate))
                    {
                        num = num2;
                        itemSlot = itemSlot2;
                    }
                }
            }
            if (!itemSlot)
            {
                AccessTools.Method(typeof(Inventory3DManager), "EnableHiddeObject")?.Invoke(__instance, null);
                //this.EnableHiddeObject();
                __result = false;
                return false;
            }
            AccessTools.Method(typeof(Inventory3DManager), "SetSelectedSlot")?.Invoke(__instance, new object[] { itemSlot });
            //this.SetSelectedSlot(itemSlot);
            if (__instance.m_SelectedSlot.IsStack())
            {
                ItemSlotStack itemSlotStack = (ItemSlotStack)__instance.m_SelectedSlot;
                int index = System.Math.Min(itemSlotStack.m_StackDummies.Count, itemSlotStack.m_Items.Count);
                __instance.m_CarriedItem.transform.position = itemSlotStack.m_StackDummies[index].transform.position;
                if (itemSlotStack.m_AdjustRotation)
                {
                    __instance.m_CarriedItem.transform.rotation = itemSlotStack.m_StackDummies[index].transform.rotation;
                }
            }
            else if (__instance.m_SelectedSlot.m_BackpackSlot && __instance.m_CarriedItem.m_InventoryHolder && (__instance.m_CarriedItem.m_Info.IsWeapon() || __instance.m_CarriedItem.m_Info.IsTool()))
            {
                Quaternion rhs = Quaternion.Inverse(__instance.m_CarriedItem.m_InventoryHolder.localRotation);
                __instance.m_CarriedItem.gameObject.transform.rotation = __instance.m_SelectedSlot.transform.rotation;
                __instance.m_CarriedItem.gameObject.transform.rotation *= rhs;
                Vector3 b2 = __instance.m_CarriedItem.transform.position - __instance.m_CarriedItem.m_InventoryHolder.position;
                __instance.m_CarriedItem.gameObject.transform.position = __instance.m_SelectedSlot.transform.position;
                __instance.m_CarriedItem.gameObject.transform.position += b2;
            }
            else
            {
                Transform transform = (__instance.m_SelectedSlot.m_WeaponRackParent != null && __instance.m_CarriedItem.m_WeaponRackHolder) ? __instance.m_CarriedItem.m_WeaponRackHolder : __instance.m_CarriedItem.m_InventoryHolder;
                if (transform)
                {
                    Quaternion rhs2 = Quaternion.Inverse(transform.localRotation);
                    __instance.m_CarriedItem.gameObject.transform.rotation = __instance.m_SelectedSlot.transform.rotation;
                    __instance.m_CarriedItem.gameObject.transform.rotation *= rhs2;
                    Vector3 b3 = __instance.m_CarriedItem.transform.position - transform.position;
                    __instance.m_CarriedItem.gameObject.transform.position = __instance.m_SelectedSlot.transform.position;
                    __instance.m_CarriedItem.gameObject.transform.position += b3;
                }
                else
                {
                    __instance.m_CarriedItem.gameObject.transform.position = __instance.m_SelectedSlot.transform.position;
                    if (__instance.m_SelectedSlot.m_AdjustRotation)
                    {
                        __instance.m_CarriedItem.gameObject.transform.rotation = __instance.m_SelectedSlot.transform.rotation;
                    }
                }
                if (__instance.m_SelectedSlot.IsArmorSlot())
                {
                    PlayerArmorModule.Get().OnDragItemToSlot((ArmorSlot)__instance.m_SelectedSlot, __instance.m_CarriedItem);
                }
            }
            __instance.m_CarriedItem.m_AttractedByItemSlot = true;
            if (__instance.m_CarriedItem.m_Info.IsDressing() || __instance.m_CarriedItem.m_Info.m_ID == ItemID.Fish_Bone || __instance.m_CarriedItem.m_Info.m_ID == ItemID.Bone_Needle)
            {
                __instance.m_CarriedItem.gameObject.SetActive(false);
            }
            __result = true;
            return false;
        }
    }
}
