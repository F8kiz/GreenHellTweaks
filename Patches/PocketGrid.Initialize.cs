using Enums;
using GHTweaks.Models;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PocketGrid), nameof(PocketGrid.Initialize))]
    internal class PocketGridInitialize
    {
        // The default in game value:
        // m_CellSize.x = 0.11387517
        // m_CellSize.y = 0.03611637

        static bool Prefix(PocketGrid __instance, GameObject cell_prefab, BackpackPocket pocket)
        {
            GridSize size;
            if (pocket == BackpackPocket.Storage)
                size = Mod.Instance.Config.PocketGridConfig.StorageGridSize;
            else if (pocket == BackpackPocket.Main || pocket == BackpackPocket.Front)
                size = Mod.Instance.Config.PocketGridConfig.BackpackGridSize;
            else 
                return true;

            if (size.RowCount <= 0 || size.ColumnCount <= 0)
            {
                Mod.Instance.WriteLog($"PocketGrid.Initialize.Prefix skip patch because the pocket size is invalid. PocketSize: {size}");
                return true;
            }

            //this.m_CellLayer = LayerMask.NameToLayer("TransparentFX");
            int cellLayerIndex = LayerMask.NameToLayer("TransparentFX");

            AccessTools.FieldRef<PocketGrid, int> fiCellLayer = AccessTools.FieldRefAccess<PocketGrid, int>("m_CellLayer");
            fiCellLayer(__instance) = cellLayerIndex;

            //this.m_Pocked = pocket;
            AccessTools.FieldRef<PocketGrid, BackpackPocket> fiPocket = AccessTools.FieldRefAccess<PocketGrid, BackpackPocket>("m_Pocked");
            fiPocket(__instance) = pocket;

            AccessTools.FieldRef<PocketGrid, Vector2> fiGridSize = AccessTools.FieldRefAccess<PocketGrid, Vector2>("m_GridSize");
            Vector2 gridSize = fiGridSize(__instance);
            Mod.Instance.WriteLog($"Default grid size {pocket}: {gridSize}");

            fiGridSize(__instance) = new Vector2(size.ColumnCount, size.RowCount);
            gridSize = fiGridSize(__instance);
            Mod.Instance.WriteLog($"New grid size: {gridSize}");

            FieldInfo fiGrid = AccessTools.Field(typeof(PocketGrid), "m_Grid");
            GameObject grid = (GameObject)fiGrid.GetValue(__instance);

            Vector3 one = Vector3.one;
            //one.x = 1f / (float)((int)this.m_GridSize.x);
            one.x = 1f / ((int)gridSize.x);

            //one.y = 1f / (float)((int)this.m_GridSize.y);
            one.y = 1f / ((int)gridSize.y);

            Mod.Instance.WriteLog($"Vector.one: {one}");

            //this.m_CellSize.x = one.x * this.m_Grid.transform.localScale.x;
            //this.m_CellSize.y = one.y * this.m_Grid.transform.localScale.y;
            Vector2 cellSize = new Vector2(one.x * grid.transform.localScale.x, one.y * grid.transform.localScale.y);
            AccessTools.FieldRef<PocketGrid, Vector2> fiCellSize = AccessTools.FieldRefAccess<PocketGrid, Vector2>("m_CellSize");
            fiCellSize(__instance) = cellSize;

            Mod.Instance.WriteLog("m_CellSize: " + fiCellSize(__instance));

            int num = 0;
            //this.m_Cells = new InventoryCell[(int)this.m_GridSize.x, (int)this.m_GridSize.y];
            InventoryCell[,] cells = new InventoryCell[(int)gridSize.x, (int)gridSize.y];
            for (int i = 0; i < (int)gridSize.y; i++)
            {
                for (int j = 0; j < (int)gridSize.x; j++)
                {
                    InventoryCell inventoryCell = new InventoryCell();
                    inventoryCell.m_IndexX = j;
                    inventoryCell.m_IndexY = i;
                    inventoryCell.pocket = pocket;
                    inventoryCell.m_Object = Object.Instantiate(cell_prefab);
                    inventoryCell.m_Object.name = pocket.ToString() + num.ToString();
                    inventoryCell.m_Object.layer = cellLayerIndex;
                    inventoryCell.m_Renderer = inventoryCell.m_Object.GetComponent<Renderer>();
                    inventoryCell.m_Renderer.enabled = false;
                    inventoryCell.m_Object.transform.parent = grid.transform;
                    inventoryCell.m_Object.transform.localRotation = Quaternion.identity;
                    inventoryCell.m_Object.transform.localScale = one;
                    Vector3 zero = Vector3.zero;
                    zero.x = 0.5f - one.x * 0.5f - one.x * (float)j;
                    zero.y = 0.5f - one.y * 0.5f - one.y * (float)i;
                    inventoryCell.m_Object.transform.localPosition = zero;
                    cells[j, i] = inventoryCell;
                    num++;
                }
            }

            AccessTools.FieldRef<PocketGrid, InventoryCell[,]> fiCells = AccessTools.FieldRefAccess<PocketGrid, InventoryCell[,]>("m_Cells");
            fiCells(__instance) = cells;

            return false;
        }

        //static void Postfix(PocketGrid __instance, BackpackPocket pocket)
        //{
        //    if (pocket != BackpackPocket.Storage && pocket != BackpackPocket.Main)
        //    {
        //        Mod.Instance.WriteLog($"Skip PocketGrid.Initialize.Postfix, invalid BackpackPocket: {pocket}");
        //        return;
        //    }

        //    float size = Mod.Instance.Config.PocketGridConfig.CellSize;
        //    if (size > 0)
        //    { 
        //        FieldInfo fiCellSize = AccessTools.Field(typeof(PocketGrid), "m_CellSize");
        //        Vector2 cellSize = (Vector2)fiCellSize.GetValue(__instance);
                
        //        Mod.Instance.WriteLog($"PocketGridInitialize.Postfix old m_CellSize: {cellSize}");
        //        fiCellSize.SetValue(__instance, new Vector2(cellSize.x * size, cellSize.y * size));
                
        //        cellSize = (Vector2)fiCellSize.GetValue(__instance);
        //        Mod.Instance.WriteLog($"PocketGridInitialize.Postfix new m_CellSize: {cellSize}");
        //    }
        //}
    }
}
