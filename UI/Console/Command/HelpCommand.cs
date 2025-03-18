using GHTweaks.UI.Console.Command.Core;
using System.Collections.Generic;

namespace GHTweaks.UI.Console.Command
{
    [Command("help", "Prints a command description")]
    internal class HelpCommand : ICommand
    {
        public string[] UsageExamples => null;

        public CommandResult Execute(CommandInfo cmd)
        {
            var buffer = new List<string>()
            {
                "<b>Information:</b>",
                "The console use a [ <b>Command</b> ][ <b>optional Parameter</b> ][ <b>optional Parameter value</b> ] Syntax.",
                "Let me explain that, lets say you want to enable/disable GHTweaks DebugMode.",
                "The <b>DebugModeEnabled</b> property is a member of the <b>Config Type</b> (class).",
                "You can access Type Properties by combining the Name of some Type and the Name of some property with a dot.",
                "With the dot notation you can tell the console where the property you want to access is located.",
                "In our case, the console can find the property in an instance of the <b>Config Type</b>.",
                "That means:",
                " -‣ the command is <b>set</b>, because we want <b>set</b> the value.",
                " -‣ the parameter is <b>Config.DebugModeEnabled</b>, because the <b>DebugModeEnabled</b> property is a member of the <b>Config Type</b>.",
                " -‣ the parameter value is <b>True</b> if you want to <b>enable</b> DebugMode and <b>False</b> if you want to <b>disable</b> DebugMode",
                "    (DebugModeEnabled is of type Boolean and therefore you can only assign True or False to it)",
                $"The complete command is {"set Config.DebugModeEnabled True".ToBoldCodeRTF()} if you want to enable DebugMode and {"set Config.DebugModeEnabled False".ToBoldCodeRTF()} otherwise.",
                "Now you can check the property by using the <b>get</b> command to get the Current value from <b>Config.DebugModeEnabled</b>.",
                $"Type in the console text field: {"get Config.DebugModeEnabled".ToBoldCodeRTF()} and the console should print: <b>Config.DebugModeEnabled: (true or false)</b>",
                "\nYou can chain multiple commands by using an a semicolon (<b>;</b>). Lets say you want to print all config values but before you want to clear the console.",
                "Then you could use two single commands <b>clear</b> and <b>print config</b> or you could combine both commands separated by a semicolon like this:",
                $" -‣ {"clear".ToBoldCodeRTF()}<b>;</b> {"print config".ToBoldCodeRTF()}\n\n"
            };

            var result = new CommandResult(cmd,CmdExecResult.Executed);
            var allCmdAttributes = CommandCache.GetAllConsoleCommandAttributes();
            if (allCmdAttributes == null)
            {
                var msg = "Found no CommandAttributes!";
                result.OutputAdd(msg);
                LogWriter.Write(msg, LogType.Error);
            }

            result.OutputAddRange(buffer);
            foreach (var attribute in allCmdAttributes)
            {
                result.OutputAdd($"Command: <b>{string.Join(", ", attribute.Commands)}</b>");
                if (!string.IsNullOrEmpty(attribute.Description))
                    result.OutputAdd($"Description: {attribute.Description}");

                result.OutputAdd($"Accepted parameter: <b>{string.Join(", ", attribute.AcceptedArguments)}</b>");

                if (CommandCache.TryGetCommand(attribute, out ICommand command))
                {
                    if (command.UsageExamples != null)
                    {
                        result.OutputAdd(new ConsoleLine($"Usage examples:"));
                        foreach (var line in command.UsageExamples)
                            result.OutputAdd(new ConsoleLine($"{Style.LineIndent}{line}"));
                    }
                }
                else
                {
                    LogWriter.Write($"Found no command, current Attribute: {string.Join(", ", attribute.Commands)}");
                }
                result.OutputAdd("");
            }

            return result;
        }
    }
}
