using GHTweaks.UI.Console.Command.Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace GHTweaks.UI.Console
{
    /*
     * Unity events are handled in partial class ConsoleWindow.UI
     */
    internal partial class ConsoleWindow
    {
        public static ConsoleWindow Instance 
        { 
            get
            {
                if (instance == null)
                {
                    instance = GreenHellGame.Instance.GetComponent<ConsoleWindow>();
                    if (instance == null)
                        instance = GreenHellGame.Instance.AddComponentWithEvent<ConsoleWindow>();
                }
                return instance;
            }    
        }
        private static ConsoleWindow instance;


        public int BufferSize
        {
            get => lineBuffer.BufferSize;
            set => lineBuffer.BufferSize = value;
        }


        private static readonly ConsoleLineBuffer lineBuffer = new ConsoleLineBuffer(500);

        private readonly List<CommandInfo> lastExecutedCommands = new List<CommandInfo>();

        private int lastExecutedCommandIndex = -1;

#if DEBUG
        private string strUserInput = "get Game.Player.m_Hit[0]";
#else
        private string strUserInput = "help";
#endif



        private ConsoleWindow() : base()
        {
            enabled = false;
        }


        public static void WriteLine(string line) => lineBuffer.Add(line);

        public static void WriteLine(string line, string color) => lineBuffer.Add(line, color);

        public static void WriteLine(Exception ex, bool writeToLog = true, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "") 
        {
            if (writeToLog)
                LogWriter.Write(ex, callerFilePath, callerMemberName);

            var callerBuffer = new List<string>();
            if (!string.IsNullOrEmpty(callerFilePath))
                callerBuffer.Add(Path.GetFileNameWithoutExtension(callerFilePath));

            if (!string.IsNullOrEmpty(callerMemberName))
                callerBuffer.Add(callerMemberName);

            var callerName = string.Join(".", callerBuffer);
            WriteLine($"[{callerName}] {ex.Message}", Style.TextColor.Error);
        }



        private void ProcessInput(string text)
        {
            LogWriter.Write($">> {text}");

            var commands = text.Split(';');
            foreach (var item in commands)
            {
                var cmd = new CommandInfo(item.Trim());
                if (string.IsNullOrEmpty(cmd.Command))
                {
                    LogWriter.Write($"No Command parsed.\nInText: {item}\nCommand: {cmd.ToParsedString()}");
                    continue;
                }

                if (ProcessCommandInternal(cmd))
                    text = null;
            }
            strUserInput = text;
        }

        private bool ProcessCommandInternal(CommandInfo cmd)
        {
            lineBuffer.Add(cmd.RawInput, Style.TextColor.UserInput);

            LogWriter.Write($"Try get: {cmd.ToParsedString()}");
            if (CommandCache.TryGetCommand(cmd, out ICommand[] commands))
            {
                LogWriter.Write($"Found possible commands: {string.Join(", ", commands.Select(x => x.GetType().Name))}");
                foreach (ICommand command in commands)
                {
                    var cmdResult = command.Execute(cmd);
                    lineBuffer.AddRange(cmdResult.Output);

                    LogWriter.Write($"{command.GetType().Name}: {cmdResult}");

                    if (cmdResult.CmdExecResult.HasFlag(CmdExecResult.Executed))
                    {
                        if (cmdResult.ConsoleAction == ConsoleAction.Close)
                            Hide();

                        if (cmdResult.ConsoleAction == ConsoleAction.Clear)
                            lineBuffer.Clear();

                        if (cmdResult.ConsoleAction == ConsoleAction.SetBufferSize)
                        {
                            if (!int.TryParse(cmdResult.Value?.ToString() ?? "", out int bufferSize))
                                lineBuffer.Add($"Invalid buffer size. The buffer size expected a number greater 0.", Style.TextColor.Error);
                            else
                                BufferSize = bufferSize;
                        }

                        AddExecutedCommand(cmd);
                        LogWriter.Write($"{command.GetType().Name} executed.");
                        return true;
                    }
                    //if (cmdResult.CmdExecResult.HasFlag(CmdExecResult.Exception|CmdExecResult.Error))
                    //    return;
                }
                LogWriter.Write("No one of the found command handler could execute the command.");
                lineBuffer.Add($"Command could not executed.", Style.TextColor.Error);
            }
            else
            {
                LogWriter.Write("Found no command.");
                lineBuffer.Add("Found no command.", Style.TextColor.Warning);
            }
            return false;
        }


        private void AddExecutedCommand(CommandInfo cmd)
        {
            if (lastExecutedCommands.Count > 19)
                lastExecutedCommands.RemoveAt(0);

            lastExecutedCommands.Add(cmd);
        }

        private CommandInfo GetPreviousExecutedCommand()
        {
            if (lastExecutedCommands.Count < 1)
                return null;

            if (++lastExecutedCommandIndex > lastExecutedCommands.Count - 1)
                lastExecutedCommandIndex = 0;

            return lastExecutedCommands[lastExecutedCommandIndex];
        }

        private CommandInfo GetNextExecutedCommand()
        {
            if (lastExecutedCommands.Count < 1)
                return null;

            if (--lastExecutedCommandIndex < 0)
                lastExecutedCommandIndex = lastExecutedCommands.Count -1;

            return lastExecutedCommands[lastExecutedCommandIndex];
        }
    }
}
