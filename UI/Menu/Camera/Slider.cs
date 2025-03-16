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
                LogWriter.Write($"Update Slider.Value of {MemberInfo.Name}: {Value}");
            }
            catch (Exception ex) 
            {
                LogWriter.Write(ex);
                Value = 0;
            }
        }
    }
}
