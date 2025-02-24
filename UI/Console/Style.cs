using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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

            public static readonly string ClassType = "#00A86B";

            public static readonly string InterfaceType = "#FADA5E";

            public static readonly string PropertyMember = "white";

            public static readonly string PrimitiveType = "#85BB65";

            public static readonly string StringType = "#CD9575";

            public static readonly string ArrayType = "#01796F";

            public static readonly string ValueType = "#0071C5";

            public static readonly string AccessModifier = "#007791";

            public static readonly string NullValue = "#0071C5";
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

        public static GUIStyle ConsoleWindow
        {
            get
            {
                if (consoleWindow == null)
                    ResetConsoleStyle();

                return consoleWindow;
            }
        }
        private static GUIStyle consoleWindow;

        public static GUIStyle CommandSuggestion
        {
            get
            {
                if (commandSuggestion == null)
                    ResetConsoleStyle();

                return commandSuggestion;
            }
        }
        private static GUIStyle commandSuggestion;

        public static GUIStyle CommandSuggestionTextArea
        {
            get
            {
                if (commandSuggestionTextArea == null)
                    ResetConsoleStyle();

                return commandSuggestionTextArea;
            }
        }
        private static GUIStyle commandSuggestionTextArea;


        public static void ResetConsoleStyle([CallerMemberName] string callerMemberName = "")
        {
            try
            {
                consoleWindow = new GUIStyle(GUI.skin.box);
                consoleWindow.normal.background = UIHelper.CreateBackgroundColorTexture(
                    (int)Math.Round(RectValues.Window.width),
                    (int)Math.Round(RectValues.Window.height),
                    Color.black
                );

                commandSuggestion = new GUIStyle(GUI.skin.textArea);
                commandSuggestion.hover.background = UIHelper.CreateBackgroundColorTexture(
                    (int)Math.Round(RectValues.Window.width),
                    (int)Math.Round(RectValues.Window.height),
                    Color.black
                );

                commandSuggestionTextArea = new GUIStyle()
                {
                    richText = true,
                    padding = new RectOffset(10, 30, 10, 10)
                };
            }
            catch(Exception ex)
            {
                LogWriter.Write($"Exception, callerMemberName: {callerMemberName}");
                LogWriter.Write(ex);
            }
        }

        public static string GetTypeColor(Type type)
        {
            if (type == null || Regex.IsMatch(type.Name, "^Nullable"))
                return TextColor.NullValue;

            if (type == typeof(string))
                return TextColor.StringType;

            if (type == typeof(bool))
                return TextColor.ValueType;

            if (type.IsPrimitive || type.IsEnum)
                return TextColor.PrimitiveType;

            if (type.IsValueType || Regex.IsMatch(type.Name, @"^(Vector(\d+)(Int)?|SpringVec\d+(Ex)?)"))
                return TextColor.ValueType;

            if (type.IsArray)
                return TextColor.ArrayType;

            if (type.IsInterface)
                return TextColor.InterfaceType;

            if (type.IsClass)
                return TextColor.ClassType;

            LogWriter.Write($"Unknown type: {type.Name}");

            return TextColor.Default;
        }
    }
}
