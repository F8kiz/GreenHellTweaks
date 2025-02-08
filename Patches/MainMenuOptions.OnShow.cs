
using GHTweaks.UI;

using HarmonyLib;

namespace GHTweaks.Patches
{
#if DEBUG
    //[HarmonyPatchCategory(PatchCategory.Default)]
    //[HarmonyPatch(typeof(MainMenuOptions), nameof(MainMenuOptions.OnShow))]
    //internal class MainMenuOptionsOnShow
    //{
    //    static void Postfix(MainMenuOptions __instance)
    //    {
    //        if (Mod.Instance.Config.DebugModeEnabled)
    //        {
    //            LogWriter.Write("MainMenuOptions.OnShow, add GHTweaks settings button");
    //            __instance.m_Game.transform.parent.AddComponentWithEvent<GHTweaksSettingsButton>();
    //        }
    //    }
    //}
#endif
}
