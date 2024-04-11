using AIs;
using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(AISoundModule), nameof(AISoundModule.Initialize))]
    internal class AISoundModuleInitialize
    {
        static void Postfix(AISoundModule __instance)
        {
#if DEBUG
            Mod.Instance.Config.AISoundModuleConfig.MouseMaxDistance = 10;
#endif

            if (Mod.Instance.Config.AISoundModuleConfig.MouseMaxDistance < 0)
                return;

            AccessTools.FieldRef<AISoundModule, AI> aiField = AccessTools.FieldRefAccess<AISoundModule, AI>("m_AI");
            AI ai = aiField(__instance);
            if (ai == null)
            {
                Mod.Instance.WriteLog("AISoundModuleInitialize.Postfix Failed to access m_AI field", LogType.Error);
                return;
            }

            if (!ai.m_ID.ToString().StartsWith("Mouse") && ai.m_ID == AI.AIID.Agouti)
                return;
            
            AccessTools.FieldRef<AISoundModule, AudioSource> audioSourceField = AccessTools.FieldRefAccess<AISoundModule, AudioSource>("m_AudioSource");
            AudioSource audioSource = audioSourceField(__instance);
            if (audioSourceField == null)
            {
                Mod.Instance.WriteLog("AISoundModuleInitialize.Postfix Failed to access m_AudioSource field", LogType.Error);
                return;
            }

#if DEBUG
            Mod.Instance.WriteLog($"AISoundModuleInitialize.Postfix Current audioSource.maxDistance: {audioSource.maxDistance}", LogType.Debug);
#endif
            audioSource.maxDistance = ((audioSource.maxDistance / 100) * Mod.Instance.Config.AISoundModuleConfig.MouseMaxDistance);
#if DEBUG
            Mod.Instance.WriteLog($"AISoundModuleInitialize.Postfix Set audioSource.maxDistance to: {audioSource.maxDistance}", LogType.Debug);
#endif
        }
    }
}
