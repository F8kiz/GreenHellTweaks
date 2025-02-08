using System;

using UnityEngine;

namespace GHTweaks.UI.Console
{
    internal class Style
    {
        public static class TextColor
        {
            /// <summary>
            /// <code>white</code>
            /// </summary>
            public static readonly string Default = "white";
            /// <summary>
            /// <code>#ff6600</code>
            /// </summary>
            public static readonly string HighLight = "#ff6600";
            /// <summary>
            /// <code>#ff6600</code>
            /// </summary>
            public static readonly string UserInput = "#ff6600";
            /// <summary>
            /// <code>yellow</code>
            /// </summary>
            public static readonly string Warning = "yellow";
            /// <summary>
            /// <code>red</code>
            /// </summary>
            public static readonly string Error = "red";
            /// <summary>
            /// <code>#8470ff</code>
            /// </summary>
            public static readonly string Code = "#8470ff";
        }

        public static class RectValues
        {

            public static readonly Rect ClientArea;

            public static readonly Rect CloseButton;

            public static readonly Rect ScrollView;

            public static readonly Rect ScrollViewContent;

            public static readonly Rect TextInput;

            public static readonly Rect CommandSuggestion;

            public static readonly Rect Window;

            static RectValues()
            {
                const int windowHeight = 300;
                Window = new Rect(0, Screen.height - windowHeight, Screen.width, windowHeight);
                CloseButton = new Rect(Window.width - 52, 2, 50, 16);
                ClientArea = new Rect(5, 25, Window.width - 10, Window.height - 27);
                TextInput = new Rect(0, 0, ClientArea.width - 10, 20);
                ScrollView = new Rect(0, 0, ClientArea.width - 12, 250);

                CommandSuggestion = new Rect(0, Screen.height - windowHeight - 225, Screen.width - 20, 225);
            }
        }

        public static readonly string LineIndent = "  ";

        public static GUIStyle ConsoleWindow {  get; private set; }

        public static GUIStyle CommandSuggestion { get; private set; }

        public static GUIStyle CommandSuggestionTextArea { get; private set; }


        static Style() 
        {
            ResetConsoleStyle();
        }

        public static void ResetConsoleStyle()
        {
            ConsoleWindow = new GUIStyle(GUI.skin.box);
            ConsoleWindow.normal.background = UIHelper.CreateBackgroundColorTexture(
                (int)Math.Round(RectValues.Window.width),
                (int)Math.Round(RectValues.Window.height),
                Color.black
            );

            CommandSuggestion = new GUIStyle(GUI.skin.textArea);
            CommandSuggestion.hover.background = UIHelper.CreateBackgroundColorTexture(
                (int)Math.Round(RectValues.Window.width),
                (int)Math.Round(RectValues.Window.height),
                Color.black
            );

            CommandSuggestionTextArea = new GUIStyle()
            {
                richText = true,
                padding = new RectOffset(10, 30, 10, 10)
            };
        }
    }
}
