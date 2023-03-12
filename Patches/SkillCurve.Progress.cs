using System;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(SkillCurve), nameof(SkillCurve.Progress))]
    internal class SkillCurveProgress
    {
        static void Postfix(ref float __result)
        {
            Mod.Instance.WriteLog($"Multiply {__result}");
            __result *= 10;
        }
    }
}
