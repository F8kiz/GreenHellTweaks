using HarmonyLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GHTweaks.UI.Console.Command.Core
{
    internal class CommandCache
    {
        private static readonly Dictionary<Type, ICommand> commandCache = new Dictionary<Type, ICommand>();

        private static CommandAttribute[] consoleCommandAttributes = null;




        public static CommandAttribute[] GetAllConsoleCommandAttributes()
        {
            if (consoleCommandAttributes != null)
                return consoleCommandAttributes;

            consoleCommandAttributes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.GetInterface(nameof(ICommand)) != null && type.GetCustomAttribute<CommandAttribute>() != null)
                .Select(type => type.GetCustomAttribute<CommandAttribute>()).ToArray();

            return consoleCommandAttributes;
        }

        public static string[] GetAllConsoleCommandNames()
        {
            var buffer = new List<string>();
            GetAllConsoleCommandAttributes().Do(x => buffer.AddRange(x.Commands));
            return buffer.Distinct().ToArray();
        }

        public static bool TryGetCommand(CommandInfo cmdInfo, out ICommand[] cmd)
        {
            if (TryGetCommandFromCache(cmdInfo, out cmd) || TryCreateRequiredInstances(cmdInfo, out cmd))
                return true;
            
            return false;
        }

        public static bool TryGetCommand<T>(out ICommand cmd) where T : ICommand
        {
            if (commandCache.TryGetValue(typeof(T), out cmd))
                return true;

            try
            {
                cmd = (T)typeof(T).CreateInstance();
                return true;
            }
            catch(Exception ex)
            {
                LogWriter.Write(ex);
            }
            return false;
        }

        public static bool TryGetCommand(CommandAttribute cmdAttribute, out ICommand cmd)
        {
            cmd = null;
            try
            {
                var kvp = commandCache.FirstOrDefault(x => cmdAttribute.Equals(x.Key.GetCustomAttribute(typeof(CommandAttribute))));
                if (kvp.Value is ICommand handler)
                {
                    cmd = handler;
                    return true;
                }

                var type = Assembly.GetExecutingAssembly().GetTypes()
                   .FirstOrDefault(x => x.GetInterface(nameof(ICommand)) != null && cmdAttribute.Equals(x.GetCustomAttribute<CommandAttribute>()));

                if (type != null)
                {
                    cmd = (ICommand)type.CreateInstance();
                    commandCache.Add(type, cmd);
                    return cmd is ICommand;
                }
            }
            catch(Exception ex)
            {
                LogWriter.Write(ex);
            }
            return false;
        }


        private static bool TryCreateRequiredInstances(CommandInfo cmdInfo, out ICommand[] newInstances)
        {
            newInstances = null;
            try
            {
                var modTypes = typeof(Mod).Assembly.GetTypes().Where(type => type.GetInterface(nameof(ICommand)) != null);
                var commandsBuffer = new List<ICommand>();
                foreach (var type in modTypes)
                {
                    var cmdAttribute = type.GetCustomAttribute<CommandAttribute>();
                    if (cmdAttribute == null)
                        continue;
#if verbose
                    LogWriter.Write($"Check command. Type: '{type.Name}', AcceptedArguments: '{string.Join(", ", cmdAttribute.AcceptedArguments)}'");
#endif
                    if (!cmdAttribute.CanExecuteCommand(cmdInfo))
                        continue;
#if verbose
                    LogWriter.Write($"Found matching command. Type: '{type.Name}', AcceptedArguments: '{string.Join(", ", cmdAttribute.AcceptedArguments)}'");
#endif
                    ICommand instance = (ICommand)type.CreateInstance();
                    commandCache.Add(type, instance);
                    commandsBuffer.Add(commandCache.Last().Value);
                }

                if (commandsBuffer.Count > 0)
                {
                    newInstances = commandsBuffer.ToArray();
                    return true;
                }
            }
            catch(Exception ex)
            {
                ConsoleWindow.WriteLine(ex);
            }
            return false;
        }

        private static bool TryGetCommandFromCache(CommandInfo cmdInfo, out ICommand[] cmd)
        {
#if verbose
            LogWriter.Write("Try get command from cache...");
#endif
            cmd = null;
            try
            {
                if ((commandCache?.Count ?? 0) < 1)
                {
#if verbose
                    LogWriter.Write("Got no cached commands so far.");
#endif
                    return false;
                }

                var buffer = new List<ICommand>();
                foreach (var kvp in commandCache)
                {
                    var cmdAttr = kvp.Key.GetCustomAttribute<CommandAttribute>();
                    if (cmdAttr == null)
                        continue;

                    if (cmdAttr.CanExecuteCommand(cmdInfo))
                        buffer.Add(kvp.Value);
                }

                if (buffer.Count > 0)
                {
                    cmd = buffer.ToArray();
                    return true;
                }
            }
            catch(Exception ex)
            {
                ConsoleWindow.WriteLine(ex);
            }
#if verbose
            LogWriter.Write("Found no matching command.");
#endif
            return false;
        }
    }
}
