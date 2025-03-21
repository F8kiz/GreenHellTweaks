﻿using GHTweaks.Models;
using GHTweaks.UI.Console;
using GHTweaks.Utilities;

using UnityEngine;

namespace GHTweaks
{
    partial class Mod
    {
        public void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.Mouse2))
            {
                Config.ConstructionConfig.CanBeAttachedToSlotBelow = !instance.Config.ConstructionConfig.CanBeAttachedToSlotBelow;
                PrintMessage($"Change CanBeAttachedToSlotBelow: {Config.ConstructionConfig.CanBeAttachedToSlotBelow}");
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
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    var instance = GreenHellGame.Instance.GetComponent<UI.Menu.Camera.Manager>() ?? GreenHellGame.Instance.AddComponentWithEvent<UI.Menu.Camera.Manager>();
                    instance.Toggle();
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
                    GameDebug.ShowMenuDebugScenario();
                return;
            }

            if (Input.GetKeyUp(KeyCode.F9))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    GameDebug.ShowMenuDebugSounds();
                else
                    MenuInGameManager.Get().ShowScreen(typeof(SaveGameMenu));
                return;
            }

            if (Input.GetKeyUp(KeyCode.F10))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    GameDebug.ShowMenuDebugSpawners();
                else
                    GameDebug.ShowMenuDebugItem();
                return;
            }

            if (Input.GetKeyUp(KeyCode.F11))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    GameDebug.ShowMenuDebugTeleport();
                else
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
            }

            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                SaveCurrentPlayerPosition();
                return;
            }

            if (Input.GetKeyUp(KeyCode.PageDown))
            {
                SerializableVector3 playerHomePosition = Config.PlayerHomePosition;
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
