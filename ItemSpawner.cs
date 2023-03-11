using Enums;
using UnityEngine;

namespace GHTweaks
{
    internal static class ItemSpawner
    {
        public static void SpawnItem(ItemID itemId)
        {
            Vector3 forward = Player.Get().GetHeadTransform().forward;
            Vector3 vector = Player.Get().GetHeadTransform().position + 0.5f * forward;
            vector = (Physics.Raycast(vector, forward, out RaycastHit raycastHit, 3f) ? raycastHit.point : (vector + forward * 2f));
            ItemsManager.Get().CreateItem(itemId, true, vector - forward * 0.2f, Player.Get().transform.rotation);
        }
    }
}
