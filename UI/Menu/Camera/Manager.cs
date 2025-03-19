﻿using CameraProjectionRenderingToolkit;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace GHTweaks.UI.Menu.Camera
{
    internal class Manager : MonoBehaviour
    {
        private Rect windowRect;

        private readonly Dictionary<string, Slider> cameraSlider;

        private readonly string[] projectionTypeNames;

        private int selectedProjectionType;

        private readonly string[] filterModeNames;

        private int selectedFilterMode;

        private readonly string[] antialiasingNames;

        private int selectedAntialiasing;

        private CameraManager cameraManager;

        private CPRT cprtInstance;

        private PostProcessLayer postProcessLayer;

        private bool isTAASupported = false;

        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;


        public Manager()
        {
            filterModeNames = Enum.GetNames(typeof(CPRT.CPRTFilterMode));
            projectionTypeNames = Enum.GetNames(typeof(CPRT.CPRTType));
            antialiasingNames = Enum.GetNames(typeof(PostProcessLayer.Antialiasing));

            cameraSlider = new Dictionary<string, Slider>()
            {
                { nameof(CPRT.fieldOfView), new Slider() },
                { nameof(CPRT.intensity), new Slider() },
                { nameof(CPRT.renderSizeFactor), new Slider() },
                { nameof(CPRT.orthographicSize), new Slider() },
                { nameof(CPRT.perspectiveOffset), new Slider() },
                { nameof(CPRT.adaptiveTolerance), new Slider() },
                { nameof(CPRT.adaptivePower), new Slider() },
                { nameof(CPRT.filterSharpen), new Slider() }
            };

            var cprtType = typeof(CPRT);
            foreach (var kvp in cameraSlider)
            {
                var field = cprtType.GetField(kvp.Key, bindingFlags);
                var rangeAttribute = field.GetCustomAttribute(typeof(RangeAttribute)) as RangeAttribute;
                kvp.Value.SetProperties(field, rangeAttribute?.min ?? 0, rangeAttribute?.max ?? 100);
                LogWriter.Write($"Set SliderValues: {kvp.Value.MemberInfo.Name} (min: {kvp.Value.MinValue}, max: {kvp.Value.MaxValue})");
            }

            enabled = false;
            LogWriter.Write("Camera.Manager instantiated...");
        }

        public void Show()
        {
            try
            {
                cameraManager = CameraManager.Get();
                cprtInstance = cameraManager?.m_CPRT;
                if (cprtInstance == null)
                {
                    LogWriter.Write($"Camera.Manager: Got no CPRT instance.");
                    Hide();
                    return;
                }
                postProcessLayer = cameraManager.m_MainCamera.GetComponent<PostProcessLayer>();
                isTAASupported = postProcessLayer.temporalAntialiasing.IsSupported();

                foreach (var kvp in cameraSlider)
                    kvp.Value.InitValue();

                var config = Mod.Instance.Config.CameraManagerConfig;
                if (config != null)
                {
                    if (config.CPRTFilterMode != null)
                    {
                        var name = config.CPRTFilterMode.ToString();
                        selectedFilterMode = Math.Max(0, Array.FindIndex(filterModeNames, x => x == name));
                    }

                    if (config.CPRTProjectionType != null)
                    {
                        var name = config.CPRTProjectionType.ToString();
                        selectedProjectionType = Math.Max(0, Array.FindIndex(projectionTypeNames, x => x == name));
                    }

                    if (config.GameAntialiasing != null)
                    {
                        var name = config.GameAntialiasing.ToString();
                        selectedAntialiasing = Math.Max(0, Array.FindIndex(antialiasingNames, x => x == name));
                    }
                }

                enabled = true;
                Mod.Instance.PauseGame(true);
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex);
            }
        }

        public void Hide()
        {
            enabled = false;
            var mod = Mod.Instance;
            mod.PauseGame(false);
            try
            {
                if (mod.Config.CameraManagerConfig == null)
                    mod.Config.CameraManagerConfig = new Configuration.CameraManagerConfig();

                mod.Config.CameraManagerConfig.CPRTFieldOfView = cameraSlider[nameof(CPRT.fieldOfView)].Value;
                mod.Config.CameraManagerConfig.CPRTIntensity = cameraSlider[nameof(CPRT.intensity)].Value;
                mod.Config.CameraManagerConfig.CPRTRenderSizeFactor = cameraSlider[nameof(CPRT.renderSizeFactor)].Value;
                mod.Config.CameraManagerConfig.CPRTOrthographicSize = cameraSlider[nameof(CPRT.orthographicSize)].Value;
                mod.Config.CameraManagerConfig.CPRTPerspectiveOffset = cameraSlider[nameof(CPRT.perspectiveOffset)].Value;
                mod.Config.CameraManagerConfig.CPRTAdaptiveTolerance = cameraSlider[nameof(CPRT.adaptiveTolerance)].Value;
                mod.Config.CameraManagerConfig.CPRTAdaptivePower = cameraSlider[nameof(CPRT.adaptivePower)].Value;
                mod.Config.CameraManagerConfig.CPRTFilterSharpen = cameraSlider[nameof(CPRT.filterSharpen)].Value;

                if (Enum.TryParse(filterModeNames[selectedFilterMode], out CPRT.CPRTFilterMode filterMode))
                    mod.Config.CameraManagerConfig.CPRTFilterMode = filterMode;
                else
                    mod.Config.CameraManagerConfig.CPRTFilterMode = null;

                if (Enum.TryParse(projectionTypeNames[selectedProjectionType], out CPRT.CPRTType type))
                    mod.Config.CameraManagerConfig.CPRTProjectionType = type;
                else
                    mod.Config.CameraManagerConfig.CPRTProjectionType = null;

                if (Enum.TryParse(antialiasingNames[selectedAntialiasing], out PostProcessLayer.Antialiasing aa))
                    mod.Config.CameraManagerConfig.GameAntialiasing = aa;
                else
                    mod.Config.CameraManagerConfig.GameAntialiasing = null;

                mod.TrySaveConfig();
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex);
            }
        }

        public void Toggle()
        {
            if (enabled)
                Hide();
            else
                Show();

            LogWriter.Write($"Toggle Camera.Manager: {enabled}");
        }

        private void Awake()
        {
            var width = Math.Max(320, Screen.width / 3);
            windowRect = new Rect(10, 10, width, Screen.height);
        }

        private void OnGUI()
        {
            GUI.skin.horizontalSlider.margin = new RectOffset(0, 0, 0, 10);
            GUI.BeginGroup(windowRect);
            GUILayout.BeginVertical(GUI.skin.box);

            GUILayout.Label($"Is Resolution supported: {cprtInstance.IsResolutionSupported}");
            GUILayout.Label($"Is TAA supported: {isTAASupported}");
            foreach (var kvp in cameraSlider)
            {
                try
                {
                    var slider = kvp.Value;
                    var currentValue = slider.MemberInfo.GetValue(cprtInstance);
                    if (currentValue == null)
                    {
                        LogWriter.Write($"Property '{slider.MemberInfo.Name}' is not initialized");
                        continue;
                    }

                    GUILayout.Label($"{slider.MemberInfo.Name}: {slider.Value}");
                    slider.Value = GUILayout.HorizontalSlider((float)currentValue, slider.MinValue, slider.MaxValue);
                    slider.MemberInfo.SetValue(cprtInstance, slider.Value);
                }
                catch (Exception ex)
                {
                    LogWriter.Write(ex);
                }
            }

            var previousFilterMode = selectedFilterMode;
            GUILayout.Label($"CPRT FilterMode: {filterModeNames[selectedFilterMode]}");
            selectedFilterMode = (int)GUILayout.HorizontalSlider(selectedFilterMode, 0, filterModeNames.Length - 1, GUILayout.MinWidth(300));
            if (selectedProjectionType != previousFilterMode && Enum.TryParse(filterModeNames[selectedFilterMode], out CPRT.CPRTFilterMode filterMode))
            {
                cprtInstance.filterMode = filterMode;
            }

            var previousProjectionType = selectedProjectionType;
            GUILayout.Label($"CPRT Projection Type: {projectionTypeNames[selectedProjectionType]}");
            selectedProjectionType = (int)GUILayout.HorizontalSlider(selectedProjectionType, 0, projectionTypeNames.Length - 1, GUILayout.MinWidth(300));
            if (selectedProjectionType != previousProjectionType && Enum.TryParse(projectionTypeNames[selectedProjectionType], out CPRT.CPRTType type))
            {
                cprtInstance.projectionType = type;
            }

            var previousAntialiasing = selectedAntialiasing;
            GUILayout.Label($"AntialiasingMode: {GetAAName(postProcessLayer.antialiasingMode)}");
            selectedAntialiasing = (int)GUILayout.HorizontalSlider(selectedAntialiasing, 0, antialiasingNames.Length - 1, GUILayout.MinWidth(300));
            if (selectedAntialiasing != previousAntialiasing && Enum.TryParse(antialiasingNames[selectedAntialiasing], out PostProcessLayer.Antialiasing antialiasing))
            {
                if (antialiasing != PostProcessLayer.Antialiasing.TemporalAntialiasing || isTAASupported)
                    postProcessLayer.antialiasingMode = antialiasing;
                else
                    LogWriter.Write("Unable to set TAA because it's not supported!");
            }

            GUILayout.EndVertical();
            GUI.EndGroup();
        }


        private string GetAAName(PostProcessLayer.Antialiasing aa)
        {
            if (aa == PostProcessLayer.Antialiasing.TemporalAntialiasing)
                return "TAA";

            if (aa == PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing)
                return "SMAA";

            if (aa == PostProcessLayer.Antialiasing.FastApproximateAntialiasing)
                return "FXAA";

            return "None";
        }

    }
}
