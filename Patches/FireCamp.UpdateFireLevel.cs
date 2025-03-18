using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.FireCamp)]
    [HarmonyPatch(typeof(Firecamp), "UpdateFireLevel")]
    internal class FireCampUpdateFireLevel
    {
        static void Prefix(Firecamp __instance)
        {
            __instance.m_EndlessFire = Mod.Instance.Config.FireCampConfig.EndlessFire;
        }
    }
}
