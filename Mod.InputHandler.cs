using Enums;
using GHTweaks.Utilities;
using GHTweaks.Serializable;

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
            }

            if (Input.GetKeyUp(KeyCode.F2))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugAI();
                    return;
                }
            }

            if (Input.GetKeyUp(KeyCode.F3))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugArena();
                    return;
                }
            }

            if (Input.GetKeyUp(KeyCode.F4))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugCamera();
                    return;
                }
            }

            if (Input.GetKeyUp(KeyCode.F5))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugSkills();
                    return;
                }
            }

            if (Input.GetKeyUp(KeyCode.F6))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugLog();
                    return;
                }
            }

            if (Input.GetKeyUp(KeyCode.F7))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugP2P();
                    return;
                }
            }

            if (Input.GetKeyUp(KeyCode.F8))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GameDebug.ShowMenuDebugScenario();
                    return;
                }
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
                GameDebug.ShowMenuDebugItem();
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

            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                SaveCurrentPlayerPosition();
                return;
            }

            if (Input.GetKeyUp(KeyCode.PageDown))
            {
                SerializeVector3 playerHomePosition = Config.PlayerHomePosition;
                if (playerHomePosition != null)
                {
                    Vector3 position = new Vector3(playerHomePosition.X, playerHomePosition.Y, playerHomePosition.Z);
                    Player.Get().Reposition(position, null);
                }
            }

            if (Config.KeyBindings.Count < 1)
                return;

            foreach(KeyBinding binding in Config.KeyBindings)
            {
                if (Input.GetKeyUp(binding.Key))
                {
                    if (!ItemSpawner.TrySpawnItem(binding.Value))
                        PrintMessage($"The item ({binding.Value}) could not be spawned.", LogType.Error);
                }
            }
        }
    }
}
