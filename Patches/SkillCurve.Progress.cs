using HarmonyLib;
using System;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(SkillCurve), nameof(SkillCurve.Progress))]
    internal class SkillCurveProgress
    {
        static void Postfix(ref float __result)
        {
            if (Mod.Instance.Config.SkillConfig.SkillProgressMultiplier > 0)
            {
                __result = Math.Max(1, __result);
                __result *= Mod.Instance.Config.SkillConfig.SkillProgressMultiplier;
            }
        }
    }
}
