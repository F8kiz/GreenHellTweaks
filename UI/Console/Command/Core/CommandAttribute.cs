using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GHTweaks.UI.Console.Command.Core
{
    internal class CommandAttribute : Attribute
    {
        /// <summary>
        /// Get a list with all accepted commands. If no commands exists an empty array is returned.
        /// </summary>
        public string[] Commands => commandMap?.Keys.ToArray() ?? new string[0];

        /// <summary>
        /// Get a list with all accepted arguments. If no command accept an argument and therefor no arguments exists an empty array is returned.
        /// </summary>
        public string[] AcceptedArguments
        {
            get
            {
                if (commandMap == null)
                    return new string[0];

                // Provide a non null return value
                var buffer = new List<string>();
                foreach (var arg in commandMap.Where(x => x.Value != null).Select(x => x.Value))
                    buffer.AddRange(arg);

                return buffer.Distinct().ToArray();
            }
        }

        public Dictionary<string, string[]> CommandMap => commandMap;

        public string Description { get; set; }

        public StringComparison Comparison { get; set; } = StringComparison.OrdinalIgnoreCase;


        private readonly string rawCommandList;

        private readonly Dictionary<string, string[]> commandMap = new Dictionary<string, string[]>();

        /// <param name="commandList">
        /// A comma-separated list of KeyValue pairs that describes which arguments one or more commands can accept.<br/>
        /// You can combine multiple commands with a '|' and you can assign one or more accepted arguments with a '='.
        /// <para>Usage examples:</para>
        /// The line describes two commands (commandOne and commandTwo) both of them accept two arguments 'argumentOne' and 'argumentTwo'.
        /// <code>
        /// commandList = commandOne|commandTwo=argumentOne|argumentTwo;
        /// </code>
        /// 
        /// The line describes a single command that accept three arguments, the asterisk matches any string, including the null string.<br/>
        /// The first argument "argumentOne*" means the argument must start with: argumentOne<br/>
        /// The second argument "*argumentTwo" means the argument must end with: argumentTwo<br/>
        /// The third argument "*argumentThree*" means the argument must contains: argumentThree
        /// <code>
        /// commandList = commandOne=argumentOne*|*argumentTwo|*argumentThree*;
        /// </code>
        /// 
        /// Command arguments are optional, the second argument does not accept any argument.
        /// <code>
        /// commandList = commandOne=argumentOne, commandWithoutArg, commandTwo=argumentTwo
        /// </code>
        /// </param>
        /// <param name="description">A short description of the command decorated with this attribute.</param>
        public CommandAttribute(string commandList, string description)
        {
            if (!string.IsNullOrEmpty(commandList))
            {
                // CommandList syntax:
                // commandOne|commandTwo=argumentOne|argumentTwo, ...
                var chunks = commandList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var chunk in chunks)
                {
                    var tmp = chunk.Split('=');
                    var commands = tmp[0].Split('|').Select(x => x.Trim()).ToArray();
                    var acceptedArgs = tmp.Length > 1 ? tmp[1].Split('|').Select(x => x.Trim()).ToArray() : null;
                    foreach (var cmd in commands)
                    {
                        if (commandMap.ContainsKey(cmd))
                        {
                            var cmdArgs = commandMap[cmd];
                            var newItems = new List<string>();
                            foreach (var arg in acceptedArgs)
                            {
                                if (!cmdArgs.Contains(arg))
                                    newItems.Add(arg);
                            }

                            if (newItems.Count > 0)
                            {
                                var args = new List<string>(cmdArgs);
                                args.AddRange(newItems);
                                commandMap[cmd] = args.ToArray();
                            }
                        }
                        else
                        {
                            commandMap.Add(cmd, acceptedArgs);
                        }
                    }
                }
            }
            Description = description;
            rawCommandList = commandList;
        }

        /// <param name="commandList">A comma-separated list of commands that the command decorated with this attribute can execute.</param>
        /// <param name="acceptedArgsList">A comma-separated list of arguments that can pass to the command decorated with this attribute.</param>
        /// <param name="description">A short description of the command decorated with this attribute.</param>
        public CommandAttribute(string commandList, string[] acceptedArgsList, string description)
        {
            var commands = commandList?.Split('|').Select(x => x.Trim()).ToArray();
            if (commands == null)
                return;

            foreach (var cmd in commands)
                commandMap.Add(cmd, acceptedArgsList);
            
            Description = description;
            rawCommandList = commandList;
        }


        public bool CanExecuteCommand(CommandInfo commandInfo)
        {
            if (commandMap.Count < 1)
                return false;

            var cmd = commandMap.FirstOrDefault(x => x.Key.Equals(commandInfo.Command, Comparison));
            if (string.IsNullOrEmpty(cmd.Key))
                return false;

            if (!commandInfo.HasArguments)
                return true;

            if ((cmd.Value?.Length ?? 0) < 1)
                return false;

            foreach(var arg in commandInfo.ArgumentMap.Keys)
            {
                foreach(var acceptedArg in cmd.Value)
                {
                    var pattern = $"^{acceptedArg.Replace("*", ".*?")}$";
                    if (Regex.IsMatch(arg, pattern, RegexOptions.IgnoreCase))
                        return true;
                }
            }

            return false;
        }

        public new bool Equals(object other)
        {
            if (other == null || !(other is CommandAttribute otherAttr))
                return false;
            
            if (other == this)
                return true;
            
            if (Comparison != otherAttr.Comparison)
                return false;

            if (Description != otherAttr.Description)
                return false;

            return commandMap.IsSameCommandMap(otherAttr.CommandMap);
        }

        public string GetRawCommandList() => rawCommandList;
    }
}
