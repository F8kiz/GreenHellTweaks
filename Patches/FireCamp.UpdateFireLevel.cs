using HarmonyLib;

using System.IO;
using System.Reflection;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(Firecamp), "Start")]
    internal class FireCampUpdateFireLevel
    {
        static bool Prefix(Firecamp __instance)
        {
            if (!Mod.Instance.Config.FireCampConfig.EndlessFire)
                return true;

            FieldInfo fiBurning = AccessTools.Field(typeof(Firecamp), nameof(Firecamp.m_Burning));
            bool isBurning = (bool)fiBurning.GetValue(__instance);
            if (!isBurning)
                return true;

            AccessTools.FieldRef<Firecamp, float> fireLevel = AccessTools.FieldRefAccess<Firecamp, float>("m_FireLevel");
            fireLevel(__instance) = 1f;
            return false;
        }
    }
}
