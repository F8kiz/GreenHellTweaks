using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(Item), nameof(Item.Initialize))]
    internal class ItemInitialize
    {
        static void Postfix(Item __instance)
        {
            if (!__instance.m_Info.m_CanBePlacedInStorage)
                return;

            float itemScale = Mod.Instance.Config.PocketGridConfig.ItemScale;
            float minItemScale = Mod.Instance.Config.PocketGridConfig.MinItemScale;
            float maxItemScale = Mod.Instance.Config.PocketGridConfig.MaxItemScale;

            if (itemScale <= minItemScale || itemScale >= maxItemScale)
                return;

            Vector3 scale = __instance.m_InventoryLocalScale;
            float x = (scale.x / 100) * 90;
            float y = (scale.y / 100) * 90;
            float z = (scale.z / 100) * 90;

            __instance.m_InventoryLocalScale = new Vector3(x,y,z);
        }
    }
}
