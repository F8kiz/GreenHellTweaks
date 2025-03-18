using CameraProjectionRenderingToolkit;
using GHTweaks.UI.Console;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(MainLevel), nameof(MainLevel.OnLoadingEndFadeOut))]
    internal class MainLevelOnSceneLoad
    {
        static void Postfix()
        {
            if(Mod.Instance.Config.CameraManagerConfig == null)
                return;
            
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var config = Mod.Instance.Config.CameraManagerConfig;
            var cprtType = typeof(CPRT);
            var cameras = Camera.main.GetComponentsInChildren<Camera>();

            for (int i = 0; i < cameras.Length; ++i)
            {
                try
                {
                    var instance = cameras[i].GetComponent<CPRT>();
                    if (instance == null)
                        continue;

                    var field = cprtType.GetField(nameof(CPRT.fieldOfView), bindingFlags);
                    field.SetValue(instance, config.CPRTFieldOfView);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.fieldOfView)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.intensity));
                    field.SetValue(instance, config.CPRTIntensity);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.intensity)}: {field.GetValue(instance)}");

                    if (config.CPRTProjectionType != null)
                        instance.projectionType = (CPRT.CPRTType)config.CPRTProjectionType;
                }
                catch (Exception ex)
                {
                    LogWriter.Write(ex);
                    ConsoleWindow.WriteLine(ex.Message);
                }
            }
        }
    }
}
