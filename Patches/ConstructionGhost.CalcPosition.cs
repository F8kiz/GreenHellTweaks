using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(ConstructionGhost), "CalcPosition")]
    internal class ConstructionGhostCalcPosition
    {
        static void Postfix(ConstructionGhost __instance)
        {
            if (!Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled || !Input.GetKey(KeyCode.LeftControl))
                return;

            Utilities.ConstructionGhostHelper.CalcPosition(ref __instance, 2.5f, 16f);
        }
    }
}
