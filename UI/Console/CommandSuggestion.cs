using GHTweaks.UI.Console.Command.Core;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using System.Text.RegularExpressions;


namespace GHTweaks.UI.Console
{
    public class CommandSuggestion
    {
        internal struct SelectedCommandSuggestion
        {
            public string UserInput;
            public string[] Suggestions;

            public void Reset()
            {
                UserInput = null;
                Suggestions = null;
            }
        }



        public bool IsVisible { get; private set; }

        public int SuggestionIndex { get; private set; } = -1;

        public static int CommandCacheSize { 
            get => commandCacheSize;
            set
            {
                if (value > 0)
                    commandCacheSize = value;
            }
        }

        private static int commandCacheSize = 100;

        private readonly ScrollInfo scrollInfo = new ScrollInfo();

        private Vector2 contentSize = Vector2.zero;

        private SelectedCommandSuggestion selectedCommandSuggestions;

        private readonly Dictionary<string, string[]> commandCache = new Dictionary<string, string[]>();

        private readonly string[] ConsoleCommandNames;


        public CommandSuggestion()
        {
            ConsoleCommandNames = CommandCache.GetAllConsoleCommandNames();
        }


        public string GetSelectedSuggestion()
        {
            if (selectedCommandSuggestions.Suggestions == null || SuggestionIndex < 0 || SuggestionIndex > selectedCommandSuggestions.Suggestions.Length - 1)
                return null;
            
            // Suggestions can have a trailing type information like: " :: Int32 (0 - 9)"
            // We need to remove them from the suggestion.
            var value = Regex.Split(selectedCommandSuggestions.Suggestions[SuggestionIndex], "::")[0];
            value = Regex.Replace(value, "<color=[^>]+>|</color>", "");

            // There are two different types of suggestions: Command suggestions and Type suggestions.
            // If we have a command suggestion (e.g. get BufferSize) we don't need to take care of a possible command and can early return the value.
            if (Regex.IsMatch(value, @"^[\w_]+(\s+\w|$)"))
                return value;

            // We have to take are about the last command
            return Regex.Replace(selectedCommandSuggestions.UserInput, @"\s+.*?$", $" {value}");
        }

        public void SelectPreviousSuggestion()
        {
            if (selectedCommandSuggestions.Suggestions == null)
                return;

            if (--SuggestionIndex < 0)
            {
                SuggestionIndex = Math.Max(0, selectedCommandSuggestions.Suggestions.Length - 1);
                scrollInfo.PositionY = contentSize.y;
            }

            if (SuggestionIndex < commandCache.Count - 11)
                scrollInfo.LineUp();
        }

        public void SelectNextSuggestion()
        {
            if (selectedCommandSuggestions.Suggestions == null)
                return;

            if (++SuggestionIndex > selectedCommandSuggestions.Suggestions.Length - 1)
            {
                scrollInfo.ScrollToTop();
                SuggestionIndex = 0;
            }

            if (SuggestionIndex > 11)
                scrollInfo.LineDown();
        }

        public void DrawCommandSuggestion(string strUserInput)
        {
            IsVisible = false;

            string[] suggestions = selectedCommandSuggestions.Suggestions;
            if (suggestions == null || selectedCommandSuggestions.UserInput != strUserInput)
            {
                suggestions = GetCommandSuggestions(strUserInput);
                if (suggestions.Length < 1)
                {
                    UpdateSelectedCommandSuggestions(null, null);
                    return;
                }
            }

            GUILayout.BeginArea(Style.RectValues.CommandSuggestion);
            GUILayout.BeginHorizontal();

            var vsSkin = GUI.skin.verticalScrollbar;
            GUI.skin.verticalScrollbar = GUIStyle.none;
            using (var scrollScope = new GUILayout.ScrollViewScope(scrollInfo.ScrollPosition, Style.CommandSuggestion))
            {
                scrollInfo.ScrollPosition = scrollScope.scrollPosition;
                var lines = string.Join("\n", suggestions.Select((x, i) => i == SuggestionIndex ? x.ToColoredRTF(Style.TextColor.HighLight, true) : x.ToColoredRTF(Style.TextColor.Default)));

                GUILayout.TextArea(lines, Style.CommandSuggestionTextArea);
                contentSize = GUI.skin.textArea.CalcSize(new GUIContent(lines));
            }
            GUI.skin.verticalScrollbar = vsSkin;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            IsVisible = true;
        }

        public string[] GetCommandSuggestions(string userInput)
        {
            try
            {
                if (string.IsNullOrEmpty(userInput))
                {
                    var result = ConsoleCommandNames.Select(x => x.Split(' ')[0]).ToArray();
                    if (selectedCommandSuggestions.UserInput != userInput)
                        UpdateSelectedCommandSuggestions(userInput, result);

                    return result;
                }

                // It's valid syntax to combine multiple commands with a semicolon.
                // We only want provide a suggestion for the last command.
                userInput = userInput.Split(';').Last().Trim();

                if (commandCache.TryGetValue(userInput, out string[] suggestions) && suggestions.Length > 0)
                {
                    UpdateSelectedCommandSuggestions(userInput, suggestions);
                    return suggestions;
                }

                var chunks = userInput.Split(' ');
               if (chunks.Length > 1)
                {
                    if (chunks[1].StartsWith("game", StringComparison.OrdinalIgnoreCase))
                    {
                        suggestions = TypeSuggestionBuilder.GetTypeSuggestionsForPath(chunks[1], "Game").Select(x =>
                        {
                            var value = x.GetFullName();
                            if (!string.IsNullOrEmpty(x.AcceptedArgumentType))
                                value += $" :: {x.AcceptedArgumentType}";

                            return value.ToSyntaxHighlightedRTF();
                        }).ToArray();
                        return AddToCache(userInput, suggestions);
                    }

                    if (Regex.IsMatch(chunks[0], @"^\s*[gs]et") && chunks[1].StartsWith("config", StringComparison.OrdinalIgnoreCase))
                    {
                        suggestions = TypeSuggestionBuilder.GetTypeSuggestionsForPath(chunks[1], "Config").Select(x =>
                        {
                            var value = x.GetFullName();
                            if (!string.IsNullOrEmpty(x.AcceptedArgumentType))
                                value += $" :: {x.AcceptedArgumentType}";

                            return value.ToSyntaxHighlightedRTF();
                        }).ToArray();
                        return AddToCache(userInput, suggestions);
                    }
                }

                var buffer = GetConsoleCommandSuggestions(userInput);
                return AddToCache(userInput, buffer.ToArray());
            }
            catch (Exception ex) 
            {
                LogWriter.Write(ex);
            }
            return new string[0];
        }

        private string[] AddToCache(string userInput, string[] suggestions)
        {
            if (commandCache.Count >= commandCacheSize)
                commandCache.Remove(commandCache.First().Key);

#if !DEBUG
            if (commandCache.ContainsKey(userInput))
                commandCache[userInput] = suggestions;
            else
                commandCache.Add(userInput, suggestions);
#else
            if (!commandCache.ContainsKey(userInput))
                commandCache.Add(userInput, new string[0]);
#endif
            UpdateSelectedCommandSuggestions(userInput, suggestions);
            return suggestions;
        }

        //private static List<string> GetGameTypeSuggestions(string typePath)
        //{
        //    var m = Regex.Match(typePath, "^game\\.(?<path>[\\w._]+)(\\.[\\w_]*)?$", RegexOptions.IgnoreCase);
        //    if (m.Success)
        //    {
        //        var path = m.Groups["path"].Value;
        //        var typeNames = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //        var search = typeNames.Last();

        //        typeNames.Remove(typeNames.Last());
        //        var startPath = "Game." + string.Join(".", typeNames);

        //        IEnumerable<Type> types = null;
        //        if (typeNames.Count > 0)
        //        {
        //            var sourceType = AssemblyHelper.GetSingleTonGameType(typeNames[0]);
        //            return TypeSuggestionBuilder.GetGameTypeSuggestionsFor(sourceType, search).Select(x => $"{startPath}.{x.GetFullName()}".ToSyntaxHighlightedRTF()).ToList();
        //        }

        //        if (types.Any())
        //        {
        //            var buffer = new List<string>();
        //            foreach(var type in types)
        //                buffer.AddRange(TypeSuggestionBuilder.GetGameTypeSuggestionsFor(type, typeNames.Last()).Select(x => x.GetFullName().ToSyntaxHighlightedRTF()));
                    
        //            return buffer;
        //        }
        //    }
        //    return new List<string>();
        //}

        private static List<string> GetConsoleCommandSuggestions(string userInput)
        {
            var chunks = userInput.Split(' ');
            var buffer = new List<string>();
            foreach (var attribute in CommandCache.GetAllConsoleCommandAttributes())
            {
                var cmdList = attribute.CommandMap.Where(x => x.Key.StartsWith(chunks[0], StringComparison.OrdinalIgnoreCase)).ToArray();
                if (!cmdList.Any())
                    continue;

                foreach (var cmd in cmdList)
                {
                    try
                    {
                        if (cmd.Value == null)
                        {
                            buffer.Add(cmd.Key);
                            continue;
                        }

                        if (chunks.Length > 1)
                        {
                            buffer.AddRange(cmd.Value
                                .Where(x => x.StartsWith(chunks[1], StringComparison.OrdinalIgnoreCase))
                                .Select(e => $"{cmd.Key} {e.Trim('*')}")
                                .Distinct()
                            );
                        }
                        else
                        {
                            buffer.AddRange(cmd.Value.Select(e => $"{cmd.Key} {e.Trim('*')}").Distinct());
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Write(ex);
                    }
                }
            }
            return buffer;
        }


        private void UpdateSelectedCommandSuggestions(string userInput, string[] suggestions)
        {
            selectedCommandSuggestions.UserInput = userInput;
            selectedCommandSuggestions.Suggestions = suggestions;
            SuggestionIndex = -1;
        }
    }
}
