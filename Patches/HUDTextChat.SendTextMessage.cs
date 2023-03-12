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
            switch (text)
            {
                case "GodMode":
                    Cheats.m_GodMode = !Cheats.m_GodMode;
                    Mod.Instance.PrintMessage($"GodMode {Cheats.m_GodMode.ToKnownState()}");
                    return false;
                case "ImmortalItems":
                    Cheats.m_ImmortalItems = !Cheats.m_ImmortalItems;
                    Mod.Instance.PrintMessage($"ImmortalItems {Cheats.m_ImmortalItems.ToKnownState()}");
                    return false;
                case "GhostMode":
                    Cheats.m_GodMode = !Cheats.m_GhostMode;
                    Mod.Instance.PrintMessage($"GhostMode {Cheats.m_GhostMode.ToKnownState()}");
                    return false;
                case "OneShotAI":
                    Cheats.m_OneShotAI = !Cheats.m_OneShotAI;
                    Mod.Instance.PrintMessage($"OneShotAI {Cheats.m_OneShotAI.ToKnownState()}");
                    return false;
                case "OneShotConstructions":
                    Cheats.m_OneShotConstructions = !Cheats.m_OneShotConstructions;
                    Mod.Instance.PrintMessage($"OneShotConstructions {Cheats.m_OneShotConstructions.ToKnownState()}");
                    return false;
                case "InstantBuild":
                    Cheats.m_InstantBuild = !Cheats.m_InstantBuild;
                    Mod.Instance.PrintMessage($"InstantBuild {Cheats.m_InstantBuild.ToKnownState()}");
                    return false;
                default:break;
            }


            text = text.ToLower();
            if (text.StartsWith("unlock"))
            {
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
                    default: return true;
                }
                return false;
            }
            return true;
        }



    }
}