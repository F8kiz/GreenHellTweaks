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

            float camY = Player.Get().GetHeadTransform().forward.y;
            Vector3 vector = __instance.gameObject.transform.position;
            vector.y +=  camY * 18f;

            float ground = Player.Get().GetWorldPosition().y;
            if (vector.y < ground && Input.GetKey(KeyCode.LeftAlt))
                vector.y += ground - 2f - vector.y;

            __instance.m_PositionOffsetMax += Input.mouseScrollDelta.y;
            __instance.gameObject.transform.position = vector;
            // base.gameObject.transform.position = vector;
        }
    }
}
