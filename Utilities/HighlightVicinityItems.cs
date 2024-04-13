using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace GHTweaks.Utilities
{
    internal class HighlightVicinityItems
    {
        public static bool Enabled 
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                if (!isEnabled)
                    ClearHighlights();
            } 
        }
        private static bool isEnabled;

        private static readonly float radius = 20f;
        private static readonly HashSet<Trigger> highlightedItems = new HashSet<Trigger>();
        

        public static bool ContainsTrigger(Trigger trigger) => highlightedItems.Contains(trigger);

        public static void OnUpdate()
        {
            if (!Enabled || !HUDItem.Get().m_Active && InputsManager.Get().m_TextInputActive)
                return;

            Vector3 playerPos = Player.Get().transform.position;
            float rangeSqr = radius * radius;

            // Find items within radius
            var outlineCamera = CameraManager.Get();
            foreach (Trigger trigger in Trigger.s_ActiveTriggers)
            {
                if (!trigger.CanBeOutlined())
                    continue;
                
                Item item = trigger as Item;
                if (item)
                {
                    // Don't highlight trees
                    if (item.m_IsTree)
                        continue;

                    // Don't highlight useless plants
                    if (item.m_IsPlant)
                    {
                        switch (item.m_Info.m_ID)
                        {
                            case Enums.ItemID.small_plant_08_cut:
                            case Enums.ItemID.small_plant_10_cut:
                            case Enums.ItemID.small_plant_13_cut:
                            case Enums.ItemID.small_plant_14_cut:
                            case Enums.ItemID.medium_plant_02_cut:
                            case Enums.ItemID.medium_plant_04_cut:
                            case Enums.ItemID.medium_plant_10_cut:
                            case Enums.ItemID.tribe_shelter_big:
                            case Enums.ItemID.tribe_shelter_small:
                                break;
                            default: continue;
                        }
                    }
                }

                // Don't highlight rivers because their highlight is glitchy
                if (trigger is LiquidSource)
                    continue;

                bool isInRange = false;
                if (!item || !item.m_InPlayersHand) // Don't highlight items held by player
                    isInRange = trigger.transform.position.Distance2DSqr(playerPos) <= rangeSqr;

                if (isInRange)
                {
                    if (!highlightedItems.Contains(trigger))
                        highlightedItems.Add(trigger);
                    
                    if (trigger.m_ForcedLayer != trigger.m_OutlineLayer)
                    {
                        trigger.m_ForcedLayer = trigger.m_OutlineLayer;
                        if (item != null)
                        {
                            if (item.m_IsPlant)
                                TryDisableCollisionWithPlayer(ref item);
                            outlineCamera?.OutlineCameraToggle(true, item);
                        }
                    }
                }
                else if (highlightedItems.Contains(trigger))
                {
                    highlightedItems.Remove(trigger);
                    trigger.m_ForcedLayer = trigger.m_DefaultLayer;

                    if (item)
                    {
                        try
                        {
                            if (item.m_IsPlant)
                                item.EnableCollisionWithPlayer();
                        }
                        catch(Exception ex)
                        {
                            Mod.Instance.WriteLog($"HighlightVicinityItems {ex.Message}", LogType.Exception);
                        }
                        outlineCamera?.OutlineCameraToggle(false, item);
                    }
                }
            }

            // Remove destroyed items from HashSet
            highlightedItems.RemoveWhere((trigger) => !trigger);
        }


        private static void ClearHighlights()
        {
            Trigger[] items = new Trigger[highlightedItems.Count];
            highlightedItems.CopyTo(items);
            highlightedItems.Clear();

            var outlineCamera = CameraManager.Get();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i])
                {
                    items[i].m_ForcedLayer = items[i].m_DefaultLayer;
                    if (items[i] is Item item)
                    {
                        item.EnableCollisionWithPlayer();
                        outlineCamera?.OutlineCameraToggle(false, item);
                    }
                }
            }
        }

        private static void TryDisableCollisionWithPlayer(ref Item item)
        {
            try
            {
                if (item == null)
                {
                    Mod.Instance.WriteLog("Unable to disable player collision item is null", LogType.Debug);
                    return;
                }

                var methodInfo = item.GetType().GetMethod("DisableCollisionsWithPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
                if (methodInfo == null)
                {
                    Mod.Instance.WriteLog("Failed to get DisableCollisionsWithPlayer method info", LogType.Debug);
                    return;
                }

                methodInfo.Invoke(item, null);
            }
            catch(Exception ex)
            {
                string exMsg = ex.InnerException?.ToString();
                if (string.IsNullOrEmpty(exMsg))
                    exMsg = ex.ToString();

                Mod.Instance.WriteLog($"HighlightVicinityItems {exMsg}", LogType.Exception);
            }
        }
    }
}
