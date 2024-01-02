using HarmonyLib;

using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PocketGrid), nameof(PocketGrid.CalcRequiredCells))]
    internal class PocketGridCalcRequiredCells
    {
        static bool Prefix(PocketGrid __instance, Item item, ref int x, ref int y)
        {
            AccessTools.FieldRef<PocketGrid, Vector2> fiGridSize = AccessTools.FieldRefAccess<PocketGrid, Vector2>("m_CellSize");
            Vector2 cellSize = fiGridSize(__instance);

            Mod.Instance.WriteLog($"PocketGrid.CalcRequiredCells inventoryLocalScale: {item.m_InventoryLocalScale}");

            if (item.m_Info.m_InventoryRotated)
            {
                x = Mathf.CeilToInt(item.m_DefaultSize.z * item.m_InventoryLocalScale.z / cellSize.x);
                y = Mathf.CeilToInt(item.m_DefaultSize.x * item.m_InventoryLocalScale.x / cellSize.y);
            }
            else
            {
                x = Mathf.CeilToInt(item.m_DefaultSize.x * item.m_InventoryLocalScale.x / cellSize.x);
                y = Mathf.CeilToInt(item.m_DefaultSize.z * item.m_InventoryLocalScale.z / cellSize.y);
            }
            return false;
        }
    }
}
