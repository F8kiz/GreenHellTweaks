using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(MainMenu), "Start")]
    internal class MainMenuStart
    {
        static void Postfix(MainMenu __instance)
        {
            __instance.m_GameVersion.text = $"{GreenHellGame.s_GameVersion} (build {GreenHellGame.GetBuildVersion()}) GHTweaks ver. {Mod.Instance.Version}";
            __instance.m_GameVersion.resizeTextForBestFit = true ;
        }
    }
}
