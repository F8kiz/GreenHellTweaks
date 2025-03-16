using CameraProjectionRenderingToolkit;

using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;


namespace GHTweaks.UI.Menu.Camera
{
    internal class Manager : MonoBehaviour
    {
        private Rect windowRect;

        private readonly List<Slider> cameraSlider;

        private readonly string[] projectionTypeNames;

        private int selectedProjectionType;

        private CPRT cprtInstance;

        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;


        public Manager()
        {
            projectionTypeNames = Enum.GetNames(typeof(CPRT.CPRTType));
            cameraSlider = new List<Slider>();
            var memberNames = new string[] {
                nameof(CPRT.fieldOfView),
                nameof(CPRT.intensity),
            };

            var cprtType = typeof(CPRT);
            foreach (var name in memberNames)
            {
                var field = cprtType.GetField(name, bindingFlags);
                var rangeAttribute = field.GetCustomAttribute(typeof(RangeAttribute)) as RangeAttribute;
                cameraSlider.Add(new Slider()
                {
                    MemberInfo = field,
                    MinValue = rangeAttribute?.min ?? 0,
                    MaxValue = rangeAttribute?.max ?? 100,
                });
            }

            enabled = false;
        }

        public void Show()
        {
            cprtInstance = CameraManager.Get()?.m_CPRT;
            if (cprtInstance == null)
            {
                LogWriter.Write($"Menu.Camera: Got no CPRT instance.");
                Hide();
                return;
            }

            cameraSlider[0].InitValue();
            cameraSlider[1].InitValue();
            if (Mod.Instance.Config.CameraManagerConfig.CPRTProjectionType != null)
            {
                var name = Mod.Instance.Config.CameraManagerConfig.CPRTProjectionType.ToString();
                selectedProjectionType = Math.Max(0, Array.FindIndex(projectionTypeNames, x => x == name));
            }

            enabled = true;
            Mod.Instance.PauseGame(true);
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

                mod.Config.CameraManagerConfig.CPRTFieldOfView = cameraSlider[0].Value;
                mod.Config.CameraManagerConfig.CPRTIntensity = cameraSlider[1].Value;
                if (Enum.TryParse(projectionTypeNames[selectedProjectionType], out CPRT.CPRTType type))
                    mod.Config.CameraManagerConfig.CPRTProjectionType = type;
                else
                    mod.Config.CameraManagerConfig.CPRTProjectionType = null;

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
            windowRect = new Rect(10, 10, 320, 200);
        }

        private void OnGUI()
        {
            GUI.skin.horizontalSlider.margin = new RectOffset(0, 0, 0, 10);
            GUI.BeginGroup(windowRect);
            GUILayout.BeginVertical(GUI.skin.box);

            for (int i = 0; i < cameraSlider.Count; ++i)
            {
                try
                {
                    var slider = cameraSlider[i];
                    var currentValue = slider.MemberInfo.GetValue(cprtInstance);
                    if (currentValue == null)
                    {
                        LogWriter.Write($"Property '{slider.MemberInfo.Name}' is not initialized");
                        continue;
                    }

                    GUILayout.Label($"{slider.MemberInfo.Name}: {cameraSlider[i].Value}");
                    cameraSlider[i].Value = GUILayout.HorizontalSlider((float)currentValue, slider.MinValue, slider.MaxValue);
                    slider.MemberInfo.SetValue(cprtInstance, slider.Value);
                }
                catch (Exception ex)
                {
                    LogWriter.Write(ex);
                }
            }

            var previousProjectionType = selectedProjectionType;
            GUILayout.Label($"CPRT Projection Type: {projectionTypeNames[selectedProjectionType]}");
            selectedProjectionType = (int)GUILayout.HorizontalSlider(selectedProjectionType, 0, projectionTypeNames.Length - 1, GUILayout.MinWidth(300));
            if (selectedProjectionType != previousProjectionType && Enum.TryParse(projectionTypeNames[selectedProjectionType], out CPRT.CPRTType type))
            {
                cprtInstance.projectionType = type;
            }

            GUILayout.EndVertical();
            GUI.EndGroup();
        }
    }
}
