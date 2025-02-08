using GHTweaks.UI.Console.Command.Core;

using System.Linq;
using System.Text.RegularExpressions;

namespace GHTweaks.UI.Console.Command
{
    //[Command("pause", "true, false", "Start or pause the Game.")]
    [Command("pause=true|false", "Start or pause the Game.")]
    internal class GamePauseCommand : ICommand
    {
        public string[] UsageExamples { get; private set; } = new string[] {
            $"Type in {"pause true".ToCodeRTF()} if you want to pause the Game.",
            $"Type in {"pause false".ToCodeRTF()} if you want to run the Game."
        };

        public CommandResult Execute(CommandInfo cmd)
        {
            var result = new CommandResult(cmd);
            if (!cmd.CommandEquals("pause") || !cmd.HasArguments)
                return result;

            var firstArgValue = cmd.GetFirstArgumentName();
            if (!Regex.IsMatch(firstArgValue, "^true|false$", RegexOptions.IgnoreCase))
            {
                result.OutputAdd("Invalid parameter value.".ToColoredRTF(Style.TextColor.Error));
                result.OutputAddRange(UsageExamples.ToList());
                result.CmdExecResult = CmdExecResult.Executed | CmdExecResult.Error;
                return result;
            }

            var pause = firstArgValue.ToLower() == "true";
            Mod.Instance.PauseGame(pause);
            result.CmdExecResult = CmdExecResult.Executed;
            return result;
        }
    }
}
