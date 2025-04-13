using AIs;
using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.AISoundModule)]
    [HarmonyPatch(typeof(AISoundModule), nameof(AISoundModule.Initialize))]
    internal class AISoundModuleInitialize
    {
        static void Postfix(AISoundModule __instance)
        {
            AccessTools.FieldRef<AISoundModule, AI> aiField = AccessTools.FieldRefAccess<AISoundModule, AI>("m_AI");
            AI ai = aiField(__instance);
            if (ai == null)
            {
                LogWriter.Write("AISoundModuleInitialize.Postfix Failed to access m_AI field", LogType.Error);
                return;
            }

            var maxDistance = -1f;
            if (ai.m_ID.ToString().StartsWith("Mouse") || ai.m_ID == AI.AIID.Agouti)
                maxDistance = Mod.Instance.Config.AISoundModuleConfig.MouseMaxDistance;
            else if (ai.m_ID == AI.AIID.Peccary)
                maxDistance = Mod.Instance.Config.AISoundModuleConfig.PeccaryMaxDistance;
            else if (ai.m_ID == AI.AIID.Capybara)
                maxDistance = Mod.Instance.Config.AISoundModuleConfig.CapybaraMaxDistance;
            else if (ai.m_ID == AI.AIID.Tapir || ai.m_ID == AI.AIID.Tapir_baby)
                maxDistance = Mod.Instance.Config.AISoundModuleConfig.TapirMaxDistance;
            else if (ai.m_ID == AI.AIID.GiantAnteater)
                maxDistance = Mod.Instance.Config.AISoundModuleConfig.GiantAntEaterMaxDistance;

            SetMaxDistance(__instance, maxDistance, ai.m_ID);
        }

        static void SetMaxDistance(AISoundModule instance, float maxDistance, AI.AIID aiID)
        {
            if (maxDistance < 0)
                return;

            AccessTools.FieldRef<AISoundModule, AudioSource> audioSourceField = AccessTools.FieldRefAccess<AISoundModule, AudioSource>("m_AudioSource");
            AudioSource audioSource = audioSourceField(instance);
            if (audioSourceField == null)
            {
                LogWriter.Write("AISoundModuleInitialize.SetMaxDistance Failed to access m_AudioSource field", LogType.Error);
                return;
            }

            var newValue = ((audioSource.maxDistance / 100f) * maxDistance);
            if (newValue >= 0 && audioSource.minDistance > newValue)
            {
                LogWriter.Write($"AISoundModuleInitialize.SetMaxDistance Set {aiID}.minDistance. Current value: {audioSource.minDistance}, new value: 0", LogType.Debug);
                audioSource.minDistance = 0f;
            }

            LogWriter.Write($"AISoundModuleInitialize.SetMaxDistance Set {aiID}.maxDistance. Current value: {audioSource.maxDistance}, new value: {newValue}", LogType.Debug);
            
            audioSource.maxDistance = newValue;

            LogWriter.Write($"AISoundModuleInitialize.SetMaxDistance {aiID}.maxDistance: {audioSource.maxDistance}", LogType.Debug);
        }
    }
}
