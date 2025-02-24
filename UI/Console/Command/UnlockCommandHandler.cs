using GHTweaks.UI.Console.Command.Core;
using GHTweaks.Utilities;

namespace GHTweaks.UI.Console.Command
{
    [Command("unlock", new string[] {
            "whole_notepad", 
            "all_items_in_notepad",
            "all_diseases_in_notepad", 
            "all_injury_states", 
            "all_injury_state_treatments", 
            "all_known_injuries",
            "map",
            "full_map"
        },
        ""
    )]
    internal class UnlockCommandHandler : ICommand
    {
        public string[] UsageExamples { get; private set; } = new string[] {
            $"Type in {"unlock whole_notepad".ToBoldCodeRTF()}",
            $"Type in {"unlock all_items_in_notepad".ToBoldCodeRTF()}",
        };

        public CommandResult Execute(CommandInfo cmd)
        {
            var result = new CommandResult(cmd);
            if (!cmd.CommandEquals("unlock") || !cmd.HasArguments)
                return result;

            switch (cmd.GetFirstArgumentName())
            {
                case "whole_notepad":
                    ItemsUnlocker.Instance.UnlockWholeNotepad();
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                case "all_items_in_notepad":
                    ItemsUnlocker.Instance.UnlockAllItemsInNotepad();
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                case "all_diseases_in_notepad":
                    ItemsUnlocker.Instance.UnlockAllDiseasesInNotepad();
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                case "all_injury_states":
                    ItemsUnlocker.Instance.UnlockAllInjuryState();
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                case "all_injury_state_treatments":
                    ItemsUnlocker.Instance.UnlockAllInjuryStateTreatment();
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                case "all_known_injuries":
                    ItemsUnlocker.Instance.UnlockAllKnownInjuries();
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                case "map":
                    ItemsUnlocker.Instance.UnlockMap(false);
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                case "full_map":
                    ItemsUnlocker.Instance.UnlockMap(true);
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;
                default:
                    result.OutputAdd($"Unknown parameter value: {cmd.GetFirstArgumentName()}", Style.TextColor.Error);
                    result.CmdExecResult = CmdExecResult.Executed | CmdExecResult.Error;
                    break;
            }

            return result;
        }
    }
}
