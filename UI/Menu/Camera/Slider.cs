using CameraProjectionRenderingToolkit;

using GHTweaks.UI.Console;

using System;
using System.Reflection;

namespace GHTweaks.UI.Menu.Camera
{
    internal class Slider
    {
        public FieldInfo MemberInfo { get; set; }

        public float MinValue { get; set; }

        public float MaxValue { get; set; }

        public float Value { get; set; }


        public Slider() { }

        public void InitValue()
        {
            var cprtInstance = CameraManager.Get()?.m_CPRT;
            if (cprtInstance == null)
            {
                LogWriter.Write($"CPRT is not initialized");
                return;
            }

            try
            {
                Value = (float)MemberInfo.GetValue(cprtInstance);
                LogWriter.Write($"Init Slider.Value {MemberInfo.Name}: {Value}");
            }
            catch (Exception ex) 
            {
                LogWriter.Write(ex);
                Value = 0;
            }
        }

        public void SetProperties(FieldInfo fieldInfo, float minValue, float maxValue)
        {
            MemberInfo = fieldInfo;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
