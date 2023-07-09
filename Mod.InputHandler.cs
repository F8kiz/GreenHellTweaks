using Enums;
using GHTweaks.Utilities;
using GreenHellTweaks.Serializable;

using UnityEngine;

namespace GHTweaks
{
    partial class Mod
    {
        public void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                Config.ConstructionConfig.CanBeAttachedToSlotBelow = !instance.Config.ConstructionConfig.CanBeAttachedToSlotBelow;
                return;
            }

            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                RainManager.Get().ToggleDebug();
                RainManager.Get().ToggleRain();
                return;
            }

            if (Input.GetKeyUp(KeyCode.F1))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugAchievements();
                    return;
                }

                ItemSpawner.SpawnItem(ItemID.Machete);
                return;
            }

            if (Input.GetKeyUp(KeyCode.F2))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugAI();
                    return;
                }

                ItemSpawner.SpawnItem(ItemID.Tribe_Spear_ArenaTribe);
                return;
            }

            if (Input.GetKeyUp(KeyCode.F3))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugArena();
                    return;
                }

                ItemSpawner.SpawnItem(ItemID.Axe_professional);
                return;
            }

            if (Input.GetKeyUp(KeyCode.F4))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugCamera();
                    return;
                }

                ItemSpawner.SpawnItem(ItemID.Modern_Axe);
                return;
            }

            if (Input.GetKeyUp(KeyCode.F5))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugSkills();
                    return;
                }

                ItemSpawner.SpawnItem(ItemID.Tribe_Bow);
                return;
            }

            if (Input.GetKeyUp(KeyCode.F6))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugLog();
                    return;
                }

                ItemSpawner.SpawnItem(ItemID.Charcoal);
                return;
            }

            if (Input.GetKeyUp(KeyCode.F7))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugP2P();
                    return;
                }
                ItemSpawner.SpawnItem(ItemID.Campfire_ash);
                return;
            }

            if (Input.GetKeyUp(KeyCode.F8))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugScenario();
                    return;
                }
                ItemSpawner.SpawnItem(ItemID.iron_ore_stone);
            }

            if (Input.GetKeyUp(KeyCode.F9))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugSounds();
                    return;
                }

                MenuInGameManager.Get().ShowScreen(typeof(SaveGameMenu));
                return;
            }

            if (Input.GetKeyUp(KeyCode.F10))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugSpawners();
                    return;
                }

                SaveCurrentPlayerPosition();
                return;
            }

            if (Input.GetKeyUp(KeyCode.F11))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugTeleport();
                    return;
                }
                HighlightVicinityItems.Enabled = !HighlightVicinityItems.Enabled;
                return;
            }

            if (Input.GetKeyUp(KeyCode.F12))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugWounds();
                    return;
                }
                return;
            }

            if (Input.GetKeyUp(KeyCode.Keypad7))
            {
                Player.Get().Reposition(Config.PlayerLastPosition, null);
                return;
            }

            if (Input.GetKeyUp(KeyCode.Keypad9))
            {
                SerializeVector3 playerHomePosition = Config.PlayerHomePosition;
                if (playerHomePosition != null)
                {
                    Vector3 position = new Vector3(playerHomePosition.X, playerHomePosition.Y, playerHomePosition.Z);
                    Player.Get().Reposition(position, null);
                }
            }
        }
    }
}
