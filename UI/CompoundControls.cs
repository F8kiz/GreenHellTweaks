using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace GHTweaks.UI
{
    internal class CompoundControls : MonoBehaviour
    {
        public static float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText)
        {
            GUI.Label(screenRect, labelText);

            // <- Push the Slider to the end of the Label
            screenRect.x += screenRect.width;

            sliderValue = GUI.HorizontalSlider(screenRect, sliderValue, 0.0f, sliderMaxValue);
            return sliderValue;
        }
    }
}
