using GHTweaks.UI.Console;
using GHTweaks.Utilities;
using HarmonyLib;

using UnityEngine;


namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Required)]
    [HarmonyPatch(typeof(GreenHellGame), "Update")]
    internal class GreenHellGameUpdate
    {
        static void Postfix()
        {
            if (Input.GetKeyUp(KeyCode.F8) && !Input.GetKey(KeyCode.LeftShift))
            {
                ConsoleWindow.Instance.Toggle();
                return;
            }

            if (Mod.Instance.Config.ConsumeKeyStrokes)
            {
                Mod.Instance.OnUpdate();
                HighlightVicinityItems.OnUpdate();
            }
        }
    }
}
