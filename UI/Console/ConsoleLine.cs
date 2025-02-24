using System.Diagnostics;

namespace GHTweaks.UI.Console
{
    public class ConsoleLine
    {
        /// <summary>
        /// Get the default ScreenHeight for a label with one line.
        /// </summary>
        public const float LINE_HEIGHT = 15f; // 27f;

        public readonly string Text;

        public readonly string Color;


        private readonly string strValue;


        [DebuggerStepThrough]
        public ConsoleLine(string text) : this(text, Style.TextColor.Default) { }

        [DebuggerStepThrough]
        public ConsoleLine(string text, string color)
        {
            Text = text;
            Color = color;
            if (string.IsNullOrEmpty(Color))
                Color = Style.TextColor.Default;

            strValue = $"<color={Color}>{text}</color>";
        }


        public override string ToString() => strValue;
    }
}
