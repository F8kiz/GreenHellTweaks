using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GHTweaks.UI.Console
{
    public class CommandInfo
    {
        public readonly string Command = null;

        public readonly Dictionary<string, string> ArgumentMap = null;

        public readonly string RawInput = null;

        public readonly bool HasArguments = false;


        public CommandInfo(string userInput) 
        {
            userInput = Regex.Replace(userInput, @"^[\r\n\s]*|[\r\n\s]*$", "");
            if (string.IsNullOrEmpty(userInput))
            {
                LogWriter.Write($"Leave {nameof(CommandInfo)} constructor, userInput is NULL or Empty.");
                return;
            }

            var parts = userInput.Split(' ').ToList();
            LogWriter.Write($"Command chunks: [ {string.Join(" ][ ", parts)} ]");

            Command = parts[0].Trim();
            RawInput = userInput;

            if (parts.Count < 1)
                return;

            ArgumentMap = new Dictionary<string, string>();

            parts.RemoveAt(0); // Remove command
            if (parts.Count == 1)
            {
                ArgumentMap.Add(parts[0].Trim(), "");
                HasArguments = true;

                LogWriter.Write($"Get command with value, skip param check. {Command} = {parts[0]}");
                return;
            }

            // Add key value pairs
            if (parts.Count % 2 == 0)
            {
                for (int i = 1; i < parts.Count; i += 2)
                {
                    var key = parts[i - 1].Trim();
                    var value = parts[i].Trim();
                    if (ArgumentMap.ContainsKey(key))
                    {
                        LogWriter.Write($"A parameter with the same already exist, ignore parameter: '{key}'", LogType.Error);
                        continue;
                    }

                    if ((value ?? "") == "*")
                        value = null;

                    ArgumentMap.Add(key, value);
                    LogWriter.Write($"Add new param: {key}={value}");
                }
            }
            else
            {
                for (int i = 0; i < parts.Count; ++i)
                {
                    var key = parts[i].Trim();
                    if (ArgumentMap.ContainsKey(key))
                    {
                        LogWriter.Write($"A parameter with the same already exist, ignore parameter: '{key}'", LogType.Error);
                        continue;
                    }

                    ArgumentMap.Add(key, null);
                    LogWriter.Write($"Add new param: {key}=null");
                }
            }

            HasArguments = ArgumentMap.Count > 0;
        }


        public bool CommandEquals(string name, StringComparison comparison = StringComparison.OrdinalIgnoreCase) 
            => string.Equals(name, Command, comparison);

        public bool CommandEquals(string name, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            => string.Equals(name, Command, comparison) && HasArguments && string.Equals(value, ArgumentMap.First().Key, comparison);

        public bool CommandEqualsAny(params string[] parameter) 
            => !string.IsNullOrEmpty(Command) && StringEqualsAny(Command, parameter);

        public bool HasArgument(string name, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            => HasArguments && ArgumentMap.Keys.FirstOrDefault(key => key.Equals(name, comparison)) != null;

        public bool FirstArgumentNameEquals(string name, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            => HasArguments && ArgumentMap.First().Key.Equals(name, comparison);

        public bool FirstArgumentNameStartsWith(string str, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            => HasArguments && ArgumentMap.First().Key.StartsWith(str, comparison);

        public bool FirstArgumentValueEquals(string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            => HasArguments && ArgumentMap.First().Value.Equals(value, comparison);

        public bool FirstArgumentValueStartsWith(string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            => HasArguments && ArgumentMap.First().Value.StartsWith(value, comparison);

        public string GetFirstArgumentName() => ArgumentMap?.First().Key ?? null;

        public string GetFirstArgumentValue() => ArgumentMap?.First().Value ?? null;
        

        public string[] GetArgumentNames()
        {
            var buffer = new List<string>();
            if (HasArguments)
                buffer.AddRange(ArgumentMap.Keys);
            
            return buffer.ToArray();
        }

        public string ToParsedString()
        {
            try
            {
                var str = $"Command: {Command ?? "NULL"}";
                if ((ArgumentMap?.Count ?? 0) < 1)
                    return str;
                
                return str + $", Parameter: {string.Join(", ", ArgumentMap.Select(x => $"{x.Key}={x.Value ?? "null"}"))}";
            }
            catch(Exception ex)
            {
                LogWriter.Write(ex.ToString());
                return "NULL";
            }
        }

        public override string ToString() => RawInput;



        private static bool IsNotDefault<T>(T value) where T : struct 
            => !value.Equals(default(T));

        private bool StringEqualsAny(string str, params string[] parameter)
        {
            if ((parameter?.Length ?? 0) < 1)
                return false;

            return parameter.FirstOrDefault(x => string.Equals(str, x, StringComparison.OrdinalIgnoreCase)) != null;
        }
    }
}
