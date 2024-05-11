using HarmonyLib;

using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Construction)]
    [HarmonyPatch(typeof(Construction), "SetUpperLevel")]
    internal class ConstructionSetUpperLevel
    {
        static void Prefix(ref int level)
        {
            if (Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled && level > 1)
                level = 1;
        }
    }

    [HarmonyPatchCategory(PatchCategory.Construction)]
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

    [HarmonyPatchCategory(PatchCategory.Construction)]
    [HarmonyPatch(typeof(ConstructionGhost), "CanBeAttachedToSlotBelow")]
    internal class ConstructionGhostCanBeAttachedToSlotBelow
    {
        static void Postfix(ref bool __result)
        {
            if (!Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled)
                return;

            if (Mod.Instance.Config.ConstructionConfig.CanBeAttachedToSlotBelow)
            {
                __result = true;
            }
        }
    }

    [HarmonyPatchCategory(PatchCategory.Construction)]
    [HarmonyPatch(typeof(ConstructionGhost), nameof(ConstructionGhost.UpdateProhibitionType))]
    internal class ConstructionGhostUpdateProhibitionType
    {
        static bool Prefix(ConstructionGhost __instance)
        {
            if (Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled)
            {
                __instance.m_ProhibitionType = ConstructionGhost.ProhibitionType.None;
                return false;
            }
            return true;
        }
    }

    [HarmonyPatchCategory(PatchCategory.Construction)]
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
