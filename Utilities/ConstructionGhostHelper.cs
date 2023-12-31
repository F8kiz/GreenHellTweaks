using UnityEngine;

namespace GHTweaks.Utilities
{
    internal class ConstructionGhostHelper
    {
        public static void CalcPosition(ref ConstructionGhost instance, float groundOffset, float multiplier)
        {
            float camY = Player.Get().GetHeadTransform().forward.y;
            Vector3 vector = instance.gameObject.transform.position;
            vector.y += camY * multiplier;

            float ground = Player.Get().GetWorldPosition().y;
            if (vector.y < ground && Input.GetKey(KeyCode.LeftAlt))
                vector.y += ground - groundOffset - vector.y;

            instance.m_PositionOffsetMax += Input.mouseScrollDelta.y;
            instance.gameObject.transform.position = vector;
        }
    }
}
