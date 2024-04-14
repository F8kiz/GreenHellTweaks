using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Required)]
    [HarmonyPatch(typeof(MainMenu), "Start")]
    internal class MainMenuStart
    {
        static void Postfix(MainMenu __instance)
        {
#if DEBUG
            __instance.m_GameVersion.text = $"{GreenHellGame.s_GameVersion} (build {GreenHellGame.GetBuildVersion()}) GHTweaks ver. {Mod.Instance.Version} Debug";
#else
            __instance.m_GameVersion.text = $"{GreenHellGame.s_GameVersion} (build {GreenHellGame.GetBuildVersion()}) GHTweaks ver. {Mod.Instance.Version}";
#endif
            __instance.m_GameVersion.resizeTextForBestFit = true;
        }
    }
}
