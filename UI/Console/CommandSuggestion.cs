using GHTweaks.Configuration.Core;
using GHTweaks.Configuration;
using GHTweaks.UI.Console.Command.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
using System.Text.RegularExpressions;


namespace GHTweaks.UI.Console
{
    internal class CommandSuggestion
    {
        internal struct TypeSuggestion
        {
            public string FullTypeName;
            public string AcceptedArgumentType;
        }


        public bool IsVisible { get; private set; }


        public string[] Suggestions => suggestions ?? new string[0];

        private string[] suggestions = null;


        private readonly ScrollInfo scrollInfo = new ScrollInfo();

        public int SuggestionIndex { get; private set; } = -1;

        private string lastUserInput = "";

        private static readonly Dictionary<string, string[]> commandSuggestionsCache = new Dictionary<string, string[]>();

        private static TypeSuggestion[] typeSuggestions = null;

        private Vector2 contentSize = Vector2.zero;


        public string GetSelectedSuggestion()
        {
            if (suggestions == null || SuggestionIndex < 0 || SuggestionIndex > suggestions.Length - 1)
                return null;

            // Suggestions can have a trailing type information like: " : 0 - 9"
            // We need to remove them from the suggestion.
            var value = suggestions[SuggestionIndex].Split(':')[0].Trim();
            if ((lastUserInput ?? "").Contains(";"))
            {
                var buf = lastUserInput.Split(';').Select(x => x.Trim()).ToArray();
                buf[buf.Length - 1] = value;
                return string.Join("; ", buf);
            }

            return value;
        }

        public void SelectPreviousSuggestion()
        {
            if (suggestions == null)
                return;

            if (--SuggestionIndex < 0)
            {
                SuggestionIndex = suggestions.Length - 1;
                scrollInfo.PositionY = contentSize.y;
            }

            if (SuggestionIndex < suggestions.Length - 11)
                scrollInfo.LineUp();
        }

        public void SelectNextSuggestion()
        {
            if (suggestions == null)
                return;

            if (++SuggestionIndex > suggestions.Length - 1)
            {
                scrollInfo.ScrollToTop();
                SuggestionIndex = 0;
            }

            LogWriter.Write($"SuggestionIndex: {SuggestionIndex}, suggestions.Length: {suggestions.Length}");

            if (SuggestionIndex > 11)
                scrollInfo.LineDown();
        }

        public void DrawCommandSuggestion(string strUserInput)
        {
            IsVisible = false;
            if (suggestions == null || strUserInput != lastUserInput)
            {
                SuggestionIndex = -1;
                suggestions = GetCommandSuggestions(strUserInput);
                if (suggestions.Length < 1)
                    return;
                
                lastUserInput = strUserInput;
            }

            GUILayout.BeginArea(Style.RectValues.CommandSuggestion);
            GUILayout.BeginHorizontal();

            var vsSkin = GUI.skin.verticalScrollbar;
            GUI.skin.verticalScrollbar = GUIStyle.none;
            using (var scrollScope = new GUILayout.ScrollViewScope(scrollInfo.ScrollPosition, Style.CommandSuggestion))
            {
                scrollInfo.ScrollPosition = scrollScope.scrollPosition;
                var lines = string.Join("\n", suggestions.Select((x, i) => i == SuggestionIndex ? x.ToColoredRTF(Style.TextColor.HighLight) : x.ToColoredRTF(Style.TextColor.Default)));
                GUILayout.TextArea(lines, Style.CommandSuggestionTextArea);
                contentSize = GUI.skin.textArea.CalcSize(new GUIContent(lines));
            }
            GUI.skin.verticalScrollbar = vsSkin;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            IsVisible = true;
        }




        public static string[] GetCommandSuggestions(string userInput)
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                // It's valid syntax to combine multiple commands with a semicolon.
                // We only want provide a suggestion for the last command.
                userInput = userInput.Split(';').Last().Trim();
            }

            if (string.IsNullOrEmpty(userInput))
                return CommandCache.GetAllConsoleCommandNames();

            if (commandSuggestionsCache.TryGetValue(userInput, out string[] cache) && cache.Length > 0)
                return cache;

            var chunks = userInput.Split(' ');
            var buffer = new List<string>();

            if (chunks.Length > 1 && chunks[1].StartsWith("Config.", StringComparison.OrdinalIgnoreCase))
            {
                foreach(var ts in GetTypeSuggestions())
                {
                    var match = Regex.Match(ts.FullTypeName, $@"^({chunks[1]}.*?)(\.|$)", RegexOptions.IgnoreCase);
                    if (!match.Success)
                        continue;

                    if (ts.FullTypeName.Equals(match.Groups[1].Value, StringComparison.OrdinalIgnoreCase))
                        buffer.Add($"{chunks[0]} {match.Groups[1].Value} : {ts.AcceptedArgumentType.ToColoredRTF(Style.TextColor.Code)}");
                    else
                        buffer.Add($"{chunks[0]} {match.Groups[1].Value}");
                }
                return buffer.Distinct().ToArray();
            }

            foreach (var attribute in CommandCache.GetAllConsoleCommandAttributes())
            {
                var cmd = attribute.CommandMap.FirstOrDefault(x => x.Key.StartsWith(chunks[0], StringComparison.OrdinalIgnoreCase) || chunks[0].StartsWith(x.Key, StringComparison.OrdinalIgnoreCase));
                if (string.IsNullOrEmpty(cmd.Key))
                    continue;

                try
                {
                    if (cmd.Value != null)
                    {
                        IEnumerable<string> suggestions=null;
                        if (chunks.Length > 1)
                        {
                            suggestions = cmd.Value
                                .Where(x => x.StartsWith(chunks[1], StringComparison.OrdinalIgnoreCase))
                                .Select(e => $"{cmd.Key} {e.Trim('*')}");

                        }
                        else
                        {
                            suggestions = cmd.Value.Select(e => $"{cmd.Key} {e.Trim('*')}");
                        }

                        if (suggestions?.Count() > 0)
                            buffer.AddRange(suggestions);
                    }
                    else 
                    {
                        buffer.Add(cmd.Key);
                    }
                }
                catch(Exception ex)
                {
                    LogWriter.Write(ex);
                }
            }

            if (commandSuggestionsCache.Count > 19)
                commandSuggestionsCache.Remove(commandSuggestionsCache.First().Key);

            var result = buffer.Distinct().ToArray();
            if (commandSuggestionsCache.ContainsKey(userInput))
                commandSuggestionsCache[userInput] = result;
            else
                commandSuggestionsCache.Add(userInput, result);

            LogWriter.Write($"Create commandSuggestionsCache --> {userInput} ==> {string.Join(", ", result)}");

            return result;
        }

        private static TypeSuggestion[] GetTypeSuggestions()
        {
            if (typeSuggestions != null)
                return typeSuggestions;

            static List<TypeSuggestion> GetSuggestions(Type source, string path)
            {
                var buffer = new List<TypeSuggestion>();
                var properties = source.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var property in properties)
                {
                    if (property.Name == nameof(IPatchConfig.HasAtLeastOneEnabledPatch))
                        continue;

                    if (property.PropertyType.GetInterface(nameof(IPatchConfig)) != null)
                    {
                        var suggestions = GetSuggestions(property.PropertyType, $"{path}.{property.Name}");
                        if (suggestions == null)
                        {
                            LogWriter.Write($"Failed to get Properties from {path}.{property.Name}");
                            continue;
                        }

                        buffer.AddRange(suggestions);
                        continue;
                    }

                    var memberProperties = property.PropertyType.GetProperties();
                    if (memberProperties.Length < 1)
                    {
                        string suffix;
                        if (property.PropertyType.Name == typeof(bool).Name)
                            suffix = "True or False";
                        else if (property.PropertyType.Name == typeof(float).Name)
                            suffix = "0.0 - 9.9";
                        else if (property.PropertyType.Name == typeof(int).Name)
                            suffix = "0 - 9";
                        else
                            suffix = property.PropertyType.Name;

                        buffer.Add(new TypeSuggestion() { 
                            FullTypeName = $"{path}.{property.Name}", 
                            AcceptedArgumentType = suffix 
                        });
                        continue;
                    }

                    //foreach (var mp in memberProperties)
                    //    buffer.AddRange(GetSuggestions(mp.PropertyType, $"{path}.{property.Name}.{mp.Name}"));
                }
                return buffer;
            }

            typeSuggestions = GetSuggestions(typeof(Config), nameof(Config)).ToArray();

            LogWriter.Write($"Type suggestions created, {typeSuggestions.Length} suggestions was created.");

            return typeSuggestions;
        }
    }
}
