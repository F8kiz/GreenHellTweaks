using CameraProjectionRenderingToolkit;
using GHTweaks.UI.Console;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
                    {
                        LogWriter.Write("Current Camera has no CPRT instance.");
                        continue;
                    }

                    var field = cprtType.GetField(nameof(CPRT.fieldOfView), bindingFlags);
                    field.SetValue(instance, config.CPRTFieldOfView);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.fieldOfView)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.intensity));
                    field.SetValue(instance, config.CPRTIntensity);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.intensity)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.renderSizeFactor));
                    field.SetValue(instance, config.CPRTRenderSizeFactor);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.renderSizeFactor)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.orthographicSize));
                    field.SetValue(instance, config.CPRTOrthographicSize);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.orthographicSize)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.perspectiveOffset));
                    field.SetValue(instance, config.CPRTPerspectiveOffset);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.perspectiveOffset)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.adaptiveTolerance));
                    field.SetValue(instance, config.CPRTAdaptiveTolerance);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.adaptiveTolerance)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.adaptivePower));
                    field.SetValue(instance, config.CPRTAdaptivePower);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.adaptivePower)}: {field.GetValue(instance)}");

                    field = cprtType.GetField(nameof(CPRT.filterSharpen));
                    field.SetValue(instance, config.CPRTFilterSharpen);
                    LogWriter.Write($"Set {instance.name}.{nameof(CPRT.filterSharpen)}: {field.GetValue(instance)}");

                    if (config.CPRTFilterMode != null)
                        instance.filterMode = (CPRT.CPRTFilterMode)config.CPRTFilterMode;

                    if (config.CPRTProjectionType != null)
                        instance.projectionType = (CPRT.CPRTType)config.CPRTProjectionType;

                    if (config.GameAntialiasing != null)
                    {
                        var postProcessLayer = CameraManager.Get().m_MainCamera?.GetComponent<PostProcessLayer>();
                        if (postProcessLayer == null)
                            LogWriter.Write("Found no PostProcessLayer instance in MainCamera.");
                        else
                            postProcessLayer.antialiasingMode = (PostProcessLayer.Antialiasing)config.GameAntialiasing;
                    }
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
