using GHTweaks.UI.Console.Command.Core;

namespace GHTweaks.UI.Console.Command
{
    [Command("toggle", new string[] {
            "GodMode",
            "ImmortalItems",
            "GhostMode",
            "OneShotAI",
            "OneShotConstructions",
            "InstantBuild",
            "PlaceEverywhere",
            "DestroyConstructionWithoutItems"
        }, ""
    )]
    internal class ToggleCheatCommandHandler : ICommand
    {
        public string[] UsageExamples { get; private set; } = new string[] {
            $"Type in {"toggle GodMode".ToCodeRTF()} to enable or disable <b>GodMode</b> cheat.",
            $"Type in {"toggle ImmortalItems".ToCodeRTF()} to enable or disable <b>ImmortalItems</b> cheat.",
        };

        public CommandResult Execute(CommandInfo cmd)
        {
            if (!cmd.CommandEquals("toggle") || !cmd.HasArguments)
                return new CommandResult(cmd);

            var result = new CommandResult(cmd, CmdExecResult.Executed);
            switch (cmd.GetFirstArgumentName())
            {
                case "GodMode":
                    Cheats.m_GodMode = !Cheats.m_GodMode;
                    PrintAndWriteMessage($"GodMode {Cheats.m_GodMode.ToKnownState()}", ref result);
                    break;
                case "ImmortalItems":
                    Cheats.m_ImmortalItems = !Cheats.m_ImmortalItems;
                    PrintAndWriteMessage($"ImmortalItems {Cheats.m_ImmortalItems.ToKnownState()}", ref result);
                    break;
                case "GhostMode":
                    Cheats.m_GhostMode = !Cheats.m_GhostMode;
                    PrintAndWriteMessage($"GhostMode {Cheats.m_GhostMode.ToKnownState()}", ref result);
                    break;
                case "OneShotAI":
                    Cheats.m_OneShotAI = !Cheats.m_OneShotAI;
                    PrintAndWriteMessage($"OneShotAI {Cheats.m_OneShotAI.ToKnownState()}", ref result);
                    break;
                case "OneShotConstructions":
                    Cheats.m_OneShotConstructions = !Cheats.m_OneShotConstructions;
                    PrintAndWriteMessage($"OneShotConstructions {Cheats.m_OneShotConstructions.ToKnownState()}", ref result);
                    break;
                case "InstantBuild":
                    Cheats.m_InstantBuild = !Cheats.m_InstantBuild;
                    PrintAndWriteMessage($"InstantBuild {Cheats.m_InstantBuild.ToKnownState()}", ref result);
                    break;
                case "PlaceEverywhere":
                    Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled = !Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled;
                    PrintAndWriteMessage($"PlaceEverywhere {Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled.ToKnownState()}", ref result);
                    break;
                case "DestroyConstructionWithoutItems":
                    Mod.Instance.Config.ConstructionConfig.DestroyConstructionWithoutItems = !Mod.Instance.Config.ConstructionConfig.DestroyConstructionWithoutItems;
                    PrintAndWriteMessage($"DestroyConstructionWithoutItems {Mod.Instance.Config.ConstructionConfig.DestroyConstructionWithoutItems.ToKnownState()}", ref result);
                    break;
                default:
                    result.OutputAdd($"Unknown parameter value: {cmd.GetFirstArgumentName()}", Style.TextColor.Error);
                    result.CmdExecResult = CmdExecResult.NotExecuted;
                    break;
            }
            return result;
        }

        private void PrintAndWriteMessage(string message, ref CommandResult result)
        {
            Mod.Instance.PrintMessage(message, LogType.Info);
            result.OutputAdd(message);
        }
    }
}
