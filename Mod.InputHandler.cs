using Enums;
using GreenHellTweaks.Serializable;
using UnityEngine;

namespace GHTweaks
{
    partial class Mod
    {
        public void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                HighlightVicinityItems.Enabled = !HighlightVicinityItems.Enabled;
                return;
            }
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                instance.Config.ConstructionConfig.CanBeAttachedToSlotBelow = !instance.Config.ConstructionConfig.CanBeAttachedToSlotBelow;
                return;
            }
            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                RainManager.Get().ToggleDebug();
                RainManager.Get().ToggleRain();
            }
            if (Input.GetKeyUp(KeyCode.F1))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ItemSpawner.SpawnItem(ItemID.Charcoal);
                    return;
                }
                ItemSpawner.SpawnItem(ItemID.Machete);
                return;
            }
            if (Input.GetKeyUp(KeyCode.F2))
            {
                ItemSpawner.SpawnItem(ItemID.Tribe_Spear_ArenaTribe);
                return;
            }
            if (Input.GetKeyUp(KeyCode.F3))
            {
                ItemSpawner.SpawnItem(ItemID.Axe_professional);
                return;
            }
            if (Input.GetKeyUp(KeyCode.F4))
            {
                ItemSpawner.SpawnItem(ItemID.Modern_Axe);
                return;
            }
            if (Input.GetKeyUp(KeyCode.F5))
            {
                ItemSpawner.SpawnItem(ItemID.Tribe_Bow);
                return;
            }
            if (Input.GetKeyUp(KeyCode.F6))
            {
                MenuInGameManager.Get().ShowScreen(typeof(SaveGameMenu));
                return;
            }
            if (Input.GetKeyUp(KeyCode.F7))
            {
                Vector3 worldPosition = Player.Get().GetWorldPosition();
                Config.PlayerHomePosition = new SerializeVector3(worldPosition.x, worldPosition.y, worldPosition.z);
                WriteLog(string.Format("Current player position: {0}", Config.PlayerHomePosition));
                if (!TrySaveConfig())
                {
                    PrintMessage("Failed to save ModConfig.xml", LogType.Error);
                    return;
                }
                PrintMessage("Saved new PlayerHomePosition", LogType.Info);
            }
            if (Input.GetKeyUp(KeyCode.F9))
            {
                MenuInGameManager.Get().ShowScreen(typeof(MenuDebugLog));
                return;
            }
            if (Input.GetKeyUp(KeyCode.F10))
            {
                MenuInGameManager.Get().ShowScreen(typeof(MenuDebugAI));
                return;
            }
            if (Input.GetKeyUp(KeyCode.F11))
            {
                MenuInGameManager.Get().ShowScreen(typeof(MenuDebugSpawners));
                return;
            }
            if (Input.GetKeyUp(KeyCode.Keypad0))
            {
                if (TryLoadConfig())
                {
                    WriteLog("Config reloaded");
                    PrintMessage("Config reloaded");
                    return;
                }
                WriteLog("Failed to reload config!", LogType.Error);
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
