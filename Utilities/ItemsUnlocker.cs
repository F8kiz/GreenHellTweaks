namespace GHTweaks.Utilities
{
    internal class ItemsUnlocker
    {
        public static ItemsUnlocker Instance 
        {
            get => instance ?? (instance = new ItemsUnlocker());    
        }
        private static ItemsUnlocker instance;


        private ItemsUnlocker() { }


        public void UnlockWholeNotepad() => ItemsManager.Get()?.UnlockWholeNotepad();

        public void UnlockAllItemsInNotepad() => ItemsManager.Get()?.UnlockAllItemsInNotepad();

        public void UnlockAllDiseasesInNotepad() => PlayerDiseasesModule.Get()?.UnlockAllDiseasesInNotepad();

        public void UnlockAllInjuryState() => PlayerInjuryModule.Get()?.UnlockAllInjuryState();

        public void UnlockAllInjuryStateTreatment() => PlayerInjuryModule.Get()?.UnlockAllInjuryStateTreatment();

        public void UnlockAllKnownInjuries() => PlayerInjuryModule.Get()?.UnlockAllKnownInjuries();

        /// <summary>
        /// Unlock the map and optional all map marker.
        /// </summary>
        /// <param name="unlockElements">If true, all map markers are also unlocked. This also unlocks achievements.</param>
        public void UnlockMap(bool unlockElements)
        {
            var mapTab = MapTab.Get();
            if (mapTab == null || mapTab.m_MapDatas.Count < 1)
            {
                Mod.Instance.PrintMessage("Map could not be unlocked", LogType.Error);
                return;
            }

            foreach(var map in mapTab.m_MapDatas)
            {
                if (!map.Value.m_Unlocked)
                    mapTab.UnlockPage(map.Key);

                if (unlockElements)
                {
                    foreach (var element in map.Value.m_Elemets)
                        mapTab.UnlockElement(element.name);
                }
            }

            Player.Get().m_MapUnlocked = true;
            Mod.Instance.PrintMessage("Map unlocked", LogType.Debug);
        }
    }
}
