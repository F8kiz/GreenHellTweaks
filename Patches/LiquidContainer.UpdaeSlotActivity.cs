using Enums;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.LiquidContainer)]
    [HarmonyPatch(typeof(LiquidContainer), "UpdateSlotsActivity")]
    internal class LiquidContainerUpdaeSlotActivity
    {
        static bool Prefix(LiquidContainer __instance)
        {
            if (__instance.m_Info.m_ID == ItemID.Coconut)
            {
                __instance.m_GetSlot.gameObject.SetActive(false);
                __instance.m_PourSlot.gameObject.SetActive(false);
            }
            if (__instance.m_InInventory)
            {
                __instance.m_GetSlot.gameObject.SetActive(false);
                __instance.m_PourSlot.gameObject.SetActive(false);
                return true;
            }
            if (__instance.m_GetSlot)
            {
                LiquidContainerInfo liquidContainerInfo = (LiquidContainerInfo)__instance.m_Info;
                if (liquidContainerInfo.m_Amount < 1f)
                {
                    __instance.m_GetSlot.gameObject.SetActive(false);
                }
                else if (liquidContainerInfo.m_Amount > 0f && liquidContainerInfo.m_LiquidType != LiquidType.Water && liquidContainerInfo.m_LiquidType != LiquidType.UnsafeWater && liquidContainerInfo.m_LiquidType != LiquidType.DirtyWater)
                {
                    __instance.m_GetSlot.gameObject.SetActive(true);
                    return false;
                }
                else if (__instance.m_GetSlot.gameObject.activeSelf)
                {
                    if (liquidContainerInfo.m_Amount < 1f)
                    {
                        __instance.m_GetSlot.gameObject.SetActive(false);
                    }
                    else if (!__instance.m_GetSlot.CanInsertItem(Inventory3DManager.Get().m_CarriedItem))
                    {
                        __instance.m_GetSlot.gameObject.SetActive(false);
                    }
                    else
                    {
                        LiquidContainerInfo liquidContainerInfo2 = (LiquidContainerInfo)Inventory3DManager.Get().m_CarriedItem.m_Info;
                        if (liquidContainerInfo2.m_Amount >= liquidContainerInfo2.m_Capacity)
                        {
                            __instance.m_GetSlot.gameObject.SetActive(false);
                        }
                    }
                }
                else if (__instance.m_GetSlot.CanInsertItem(Inventory3DManager.Get().m_CarriedItem))
                {
                    if (Inventory3DManager.Get().m_CarriedItem.m_Info.m_ID == ItemID.Coconut)
                    {
                        __instance.m_GetSlot.gameObject.SetActive(false);
                    }
                    else
                    {
                        LiquidContainerInfo liquidContainerInfo3 = (LiquidContainerInfo)Inventory3DManager.Get().m_CarriedItem.m_Info;
                        if (liquidContainerInfo3.m_Amount < liquidContainerInfo3.m_Capacity)
                        {
                            __instance.m_GetSlot.gameObject.SetActive(true);
                        }
                    }
                }
            }
            if (__instance.m_PourSlot)
            {
                if (__instance.m_PourSlot.gameObject.activeSelf)
                {
                    if (!Inventory3DManager.Get().m_CarriedItem)
                    {
                        __instance.m_PourSlot.gameObject.SetActive(false);
                        return true;
                    }
                    if (Inventory3DManager.Get().m_CarriedItem.m_Info.IsLiquidContainer() && ((LiquidContainerInfo)Inventory3DManager.Get().m_CarriedItem.m_Info).m_Amount < 1f)
                    {
                        __instance.m_PourSlot.gameObject.SetActive(false);
                        return true;
                    }
                    if (!__instance.m_PourSlot.CanInsertItem(Inventory3DManager.Get().m_CarriedItem))
                    {
                        __instance.m_PourSlot.gameObject.SetActive(false);
                        return true;
                    }
                    LiquidContainerInfo liquidContainerInfo4 = (LiquidContainerInfo)__instance.m_Info;
                    if (liquidContainerInfo4.m_Amount >= liquidContainerInfo4.m_Capacity)
                    {
                        __instance.m_PourSlot.gameObject.SetActive(false);
                        return true;
                    }
                }
                else if (__instance.m_PourSlot.CanInsertItem(Inventory3DManager.Get().m_CarriedItem))
                {
                    if ((__instance.m_Info.m_ID == ItemID.Bidon || __instance.m_Info.m_ID == ItemID.Coconut_Bidon || __instance.m_Info.m_ID == ItemID.clay_bidon) && Inventory3DManager.Get().m_CarriedItem.m_Info.IsLiquidContainer() && ((LiquidContainerInfo)Inventory3DManager.Get().m_CarriedItem.m_Info).m_Amount >= 1f && ((LiquidContainerInfo)Inventory3DManager.Get().m_CarriedItem.m_Info).m_LiquidType != LiquidType.Water && ((LiquidContainerInfo)Inventory3DManager.Get().m_CarriedItem.m_Info).m_LiquidType != LiquidType.UnsafeWater && ((LiquidContainerInfo)Inventory3DManager.Get().m_CarriedItem.m_Info).m_LiquidType != LiquidType.DirtyWater)
                    {
                        __instance.m_PourSlot.gameObject.SetActive(false);
                        return true;
                    }
                    LiquidContainerInfo liquidContainerInfo5 = (LiquidContainerInfo)__instance.m_Info;
                    if (liquidContainerInfo5.m_Amount < liquidContainerInfo5.m_Capacity)
                    {
                        __instance.m_PourSlot.gameObject.SetActive(true);
                    }
                }
            }
            return true;
        }
    }
}
