using Enums;
using System;

using UnityEngine;

namespace GHTweaks.Utilities
{
    internal static class ItemSpawner
    {
        public static void SpawnItem(ItemID itemId, int amount=1)
        {
            Vector3 forward = Player.Get().GetHeadTransform().forward;
            Vector3 vector = Player.Get().GetHeadTransform().position + 0.5f * forward;
            vector = (Physics.Raycast(vector, forward, out RaycastHit raycastHit, 3f) ? raycastHit.point : (vector + forward * 2f));

            for(int i = 0; i < amount; ++i)
                ItemsManager.Get().CreateItem(itemId, true, vector - forward * 0.2f, Player.Get().transform.rotation);

            LogWriter.Write($"Spawned {itemId} {amount} times");
        }

        public static bool TrySpawnItem(string itemName, int amount=1)
        {
            if (!string.IsNullOrEmpty(itemName) && Enum.TryParse(itemName, out ItemID itemId))
            {
                SpawnItem(itemId, amount);
                return true;
            }
            return false;
        }
    }
}
