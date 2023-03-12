using System;

namespace GHTweaks.Utilities
{
    internal class GameDebug
    {
        public static void ShowMenuDebugAchievements() => ShowScreen(typeof(MenuDebugAchievements));

        public static void ShowMenuDebugAI() => ShowScreen(typeof(MenuDebugAI));

        public static void ShowMenuDebugArena() => ShowScreen(typeof(MenuDebugArena));

        public static void ShowMenuDebugCamera() => ShowScreen(typeof(MenuDebugCamera));

        //public static void ShowMenuDebugDialogs() => ShowScreen(typeof(MenuDebugDialogs));

        public static void ShowMenuDebugItem() => ShowScreen(typeof(MenuDebugItem));

        public static void ShowMenuDebugLog() => ShowScreen(typeof(MenuDebugLog));

        public static void ShowMenuDebugP2P() => ShowScreen(typeof(MenuDebugP2P));

        public static void ShowMenuDebugScenario() => ShowScreen(typeof(MenuDebugScenario));

        //public static void ShowMenuDebugScenarioDialogs() => ShowScreen(typeof(MenuDebugScenarioDialogs));

        //public static void ShowMenuDebugScreen() => ShowScreen(typeof(MenuDebugScreen));

        //public static void ShowMenuDebugSelectMode() => ShowScreen(typeof(MenuDebugSelectMode));

        //public static void ShowMenuDebugSkills() => ShowScreen(typeof(MenuDebugSkills));

        public static void ShowMenuDebugSounds() => ShowScreen(typeof(MenuDebugSounds));

        public static void ShowMenuDebugSpawners() => ShowScreen(typeof(MenuDebugSpawners));

        public static void ShowMenuDebugTeleport() => ShowScreen(typeof(MenuDebugTeleport));

        public static void ShowMenuDebugWounds() => ShowScreen(typeof(MenuDebugWounds));




        private static void ShowScreen(Type screen)
        {
            try
            {
                MenuInGameManager mgr = MenuInGameManager.Get();
                if (mgr == null)
                {
                    Mod.Instance.WriteLog("Failed to get MenuInGameManager instance.", LogType.Error);
                    return;
                }
                mgr.ShowScreen(screen);
            }
            catch (Exception ex) 
            {
                Mod.Instance.WriteLog(ex.Message, LogType.Exception);
            }
        }
    }
}
