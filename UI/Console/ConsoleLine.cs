namespace GHTweaks.UI.Console
{
    internal class ConsoleLine
    {
        /// <summary>
        /// Get the default ScreenHeight for a label with one line.
        /// </summary>
        public const float LINE_HEIGHT = 15f; // 27f;

        public readonly string Text;

        public readonly string Color;


        private readonly string strValue;


        public ConsoleLine(string text) : this(text, Style.TextColor.Default) { }

        public ConsoleLine(string text, string color)
        {
            Text = text;
            Color = color;
            strValue = $"<color={color}>{text}</color>";
        }


        public override string ToString() => strValue;
    }
}
