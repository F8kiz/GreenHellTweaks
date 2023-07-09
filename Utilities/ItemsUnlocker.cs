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
    }
}
