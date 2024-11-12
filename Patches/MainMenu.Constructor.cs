using GHTweaks.UI;

using HarmonyLib;

using System.Reflection;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(MainMenu), ".ctor", MethodType.Constructor)]
    internal class MainMenuContructor
    {
        static void Postfix(MainMenu __instance)
        {
            if (Mod.Instance.Config.SkipIntro)
            {
                var fieldNames = new string[] {
                    "m_FadeInDuration",
                    "m_FadeOutDuration",
                    "m_FadeOutSceneDuration",
                    "m_CompanyLogoDuration",
                    "m_GameLogoDuration",
                    "m_BlackScreenDuration"
                };
                var mainMenuType = typeof(MainMenu);
                foreach (string fieldName in fieldNames)
                {
                    FieldInfo fieldInfo = AccessTools.Field(mainMenuType, fieldName);
                    fieldInfo.SetValue(__instance, 0);
                }
            }
        }
    }
}
