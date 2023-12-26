using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PocketGrid), nameof(PocketGrid.Initialize))]
    internal class PocketGridInitialize
    {
        static bool Prefix(PocketGrid __instance)
        {
            float size = Mod.Instance.Config.PocketGridConfig.CellSize;
            if (size > 0)
            {
                __instance.m_GridSize = new Vector2(size, size);
            }
            return true;
        }

        static void Postfix(PocketGrid __instance)
        {
            float size = Mod.Instance.Config.PocketGridConfig.CellSize;
            if (size > 0)
            { 
                FieldInfo fiCellSize = AccessTools.Field(typeof(PocketGrid), "m_CellSize");
                Vector2 cellSize = (Vector2)fiCellSize.GetValue(__instance);
                //Mod.Instance.WriteLog($"Old m_CellSize: {cellSize}");
                fiCellSize.SetValue(__instance, new Vector2(cellSize.x * size, cellSize.y * size));
                //cellSize = (Vector2)fiCellSize.GetValue(__instance);
                //Mod.Instance.WriteLog($"New m_CellSize: {cellSize}");
            }
        }
    }
}
