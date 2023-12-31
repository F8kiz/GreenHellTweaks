using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(ConstructionGhostWaterSurface), "CalcPosition")]
    internal class ConstructionGhostWaterSurfaceCalcPosition
    {
        static void Postfix(ConstructionGhost __instance)
        {
            if (!Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled || !Input.GetKey(KeyCode.LeftControl))
                return;

            Utilities.ConstructionGhostHelper.CalcPosition(ref __instance, 2f, 18f);
        }
    }
}
