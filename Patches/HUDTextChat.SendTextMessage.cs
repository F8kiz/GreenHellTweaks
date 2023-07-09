using GHTweaks.Utilities;

using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(HUDTextChat), "SendTextMessage")]
    internal class HUDTextChatSendTextMessage
    {
        static bool Prefix(HUDTextChat __instance)
        {
            string text = __instance.m_Field.text;
            if (HandleCheatInput(text))
                return false;

            text = text.ToLower();
            if (HandleUnlockInput(text))
                return false;

            if (HandleSpawnItemInput(text))
                return false;

            return true;
        }


        static bool HandleCheatInput(string text) 
        {
            switch (text)
            {
                case "GodMode":
                    Cheats.m_GodMode = !Cheats.m_GodMode;
                    Mod.Instance.PrintMessage($"GodMode {Cheats.m_GodMode.ToKnownState()}", LogType.Info);
                    return true;
                case "ImmortalItems":
                    Cheats.m_ImmortalItems = !Cheats.m_ImmortalItems;
                    Mod.Instance.PrintMessage($"ImmortalItems {Cheats.m_ImmortalItems.ToKnownState()}", LogType.Info);
                    return true;
                case "GhostMode":
                    Cheats.m_GhostMode = !Cheats.m_GhostMode;
                    Mod.Instance.PrintMessage($"GhostMode {Cheats.m_GhostMode.ToKnownState()}", LogType.Info);
                    return true;
                case "OneShotAI":
                    Cheats.m_OneShotAI = !Cheats.m_OneShotAI;
                    Mod.Instance.PrintMessage($"OneShotAI {Cheats.m_OneShotAI.ToKnownState()}", LogType.Info);
                    return true;
                case "OneShotConstructions":
                    Cheats.m_OneShotConstructions = !Cheats.m_OneShotConstructions;
                    Mod.Instance.PrintMessage($"OneShotConstructions {Cheats.m_OneShotConstructions.ToKnownState()}", LogType.Info);
                    return true;
                case "InstantBuild":
                    Cheats.m_InstantBuild = !Cheats.m_InstantBuild;
                    Mod.Instance.PrintMessage($"InstantBuild {Cheats.m_InstantBuild.ToKnownState()}", LogType.Info);
                    return true;
                case "reload config":
                    Mod.Instance.ReloadConfig();
                    return true;
                default: return false;
            }
        }

        static bool HandleUnlockInput(string text)
        {
            if (!text.StartsWith("unlock"))
                return false;

            switch (text)
            {
                case "unlock whole notepad":
                    ItemsUnlocker.Instance.UnlockWholeNotepad();
                    break;
                case "unlock all items in notepad":
                    ItemsUnlocker.Instance.UnlockAllItemsInNotepad();
                    break;
                case "unlock all diseases in notepad":
                    ItemsUnlocker.Instance.UnlockAllDiseasesInNotepad();
                    break;
                case "unlock all injury states":
                    ItemsUnlocker.Instance.UnlockAllInjuryState();
                    break;
                case "unlock all injury state treatments":
                    ItemsUnlocker.Instance.UnlockAllInjuryStateTreatment();
                    break;
                case "unlock all known injuries":
                    ItemsUnlocker.Instance.UnlockAllKnownInjuries();
                    break;
                default: return false;
            }
            return true;
        }

        static bool HandleSpawnItemInput(string text)
        {
            if (!text.StartsWith("spawn"))
                return false;

            switch(text)
            {
                case "spawn raw meat":
                    ItemSpawner.SpawnItem(Enums.ItemID.Meat_Raw);
                    break;

                case "spawn cooked meat":
                    ItemSpawner.SpawnItem(Enums.ItemID.Meat_Cooked);
                    break;

                case "spawn mushrooms":
                    ItemSpawner.SpawnItem(Enums.ItemID.marasmius_haematocephalus);
                    ItemSpawner.SpawnItem(Enums.ItemID.indigo_blue_leptonia);
                    ItemSpawner.SpawnItem(Enums.ItemID.Gerronema_retiarium);
                    ItemSpawner.SpawnItem(Enums.ItemID.Gerronema_viridilucens);
                    break;

                case "spawn fruits":
                    ItemSpawner.SpawnItem(Enums.ItemID.Coconut_flesh);
                    ItemSpawner.SpawnItem(Enums.ItemID.Guanabana_Fruit);
                    ItemSpawner.SpawnItem(Enums.ItemID.Cocona_fruit);
                    ItemSpawner.SpawnItem(Enums.ItemID.monstera_deliciosa_fruit);
                    break;

                case "spawn root":
                    ItemSpawner.SpawnItem(Enums.ItemID.Malanga_bulb);
                    ItemSpawner.SpawnItem(Enums.ItemID.Cassava_bulb);
                    break;

                default : return false;
            }

            return true;
        }
    }
}