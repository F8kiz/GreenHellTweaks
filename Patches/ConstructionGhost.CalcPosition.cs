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

            float py = Player.Get().GetHeadTransform().forward.y;
            Vector3 vector = __instance.gameObject.transform.position;
            vector.y += py * 16f;

            float ground = Player.Get().GetWorldPosition().y;
            if (vector.y < ground && Input.GetKey(KeyCode.LeftAlt))
                vector.y += ground - 2.5f - vector.y;

           __instance.m_PositionOffsetMax += Input.mouseScrollDelta.y;
            __instance.gameObject.transform.position = vector;
            // base.gameObject.transform.position = vector;
        }
    }
}
