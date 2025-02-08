using GHTweaks.UI.Console.Command.Core;
using System.Linq;

namespace GHTweaks.UI.Console.Command
{
    //[Command("get, set, clear, close, reset", "BufferSize, Style", "")]
    [Command("get|set=BufferSize, clear, close, reset=Style", "")]
    internal class ConsoleCommandHandler : ICommand
    {
        public string[] UsageExamples { get; private set; } = new string[] {
            $"Type in {"clear".ToCodeRTF()} clears the console line buffer.",
            $"Type in {"close".ToCodeRTF()} Close the console.",
            $"Type in {"get BufferSize".ToCodeRTF()} to print the current console line buffer size.",
            $"Type in {"set BufferSize 100".ToCodeRTF()} to set the console line buffer size to 100.",
            $"Type in {"reset Style".ToCodeRTF()} to reset the console style. Sometimes the console may get a transparent background. If this is the case for you, you can use this command."
        };

        public CommandResult Execute(CommandInfo cmd)
        {
            var result = new CommandResult(cmd, CmdExecResult.Executed);
            if (cmd.CommandEquals("clear"))
            {
                result.ConsoleAction = ConsoleAction.Clear;
                return result;
            }

            if (cmd.CommandEquals("close"))
            {
                result.ConsoleAction = ConsoleAction.Close;
                return result;
            }

            if (cmd.CommandEquals("reset", "style"))
            {
                result.OutputAdd("Reset console style");
                Style.ResetConsoleStyle();
                return result;
            }

            if (cmd.CommandEquals("set") && cmd.FirstArgumentNameEquals("BufferSize"))
            {
                if (int.TryParse(cmd.ArgumentMap.First().Value.ToString(), out int value))
                {
                    result.Value = value;
                    result.OutputAdd($"Set BufferSize: {value}");
                    result.ConsoleAction = ConsoleAction.SetBufferSize;
                }
                else
                {
                    result.CmdExecResult = CmdExecResult.Executed|CmdExecResult.Error;
                    result.OutputAdd("Invalid parameter value, expected a number.");
                }
                return result;
            }

            if (cmd.CommandEquals("get", "BufferSize"))
            {
                result.OutputAdd($"BufferSize: {ConsoleWindow.Instance.BufferSize}");
                return result;
            }

            result.CmdExecResult = CmdExecResult.NotExecuted;
            return result;
        }
    }
}
