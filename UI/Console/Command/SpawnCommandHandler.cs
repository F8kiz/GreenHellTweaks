

using GHTweaks.UI.Console.Command.Core;
using GHTweaks.Utilities;

namespace GHTweaks.UI.Console.Command
{
    //[Command("spawn", "raw_meat, cooked_meat, mushrooms, fruits, root", "")]
    [Command("spawn=raw_meat|cooked_meat|mushrooms|fruits|root", "")]
    internal class SpawnCommandHandler : ICommand
    {
        public string[] UsageExamples { get; private set; } = new string[] {
            $"Type in {"spawn raw_meat".ToCodeRTF()} to spawn some raw meat.",
            $"Type in {"spawn cooked_meat".ToCodeRTF()} to spawn some cooked meat.",
        };

        public CommandResult Execute(CommandInfo cmd)
        {
            var result = new CommandResult(cmd);
            if (!cmd.CommandEquals("spawn") || !cmd.HasArguments)
            {
                result.OutputAdd("No spawn command");
                return result;
            }

            if (!int.TryParse((cmd.GetFirstArgumentValue() ?? "1"), out int count))
                count = 1;

            switch (cmd.GetFirstArgumentName())
            {
                case "raw_meat":
                    ItemSpawner.SpawnItem(Enums.ItemID.Meat_Raw, count);
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;

                case "cooked_meat":
                    ItemSpawner.SpawnItem(Enums.ItemID.Meat_Cooked, count);
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;

                case "mushrooms":
                    ItemSpawner.SpawnItem(Enums.ItemID.marasmius_haematocephalus, count);
                    ItemSpawner.SpawnItem(Enums.ItemID.indigo_blue_leptonia, count);
                    ItemSpawner.SpawnItem(Enums.ItemID.Gerronema_retiarium, count);
                    ItemSpawner.SpawnItem(Enums.ItemID.Gerronema_viridilucens, count);
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;

                case "fruits":
                    ItemSpawner.SpawnItem(Enums.ItemID.Coconut_flesh, count);
                    ItemSpawner.SpawnItem(Enums.ItemID.Guanabana_Fruit, count);
                    ItemSpawner.SpawnItem(Enums.ItemID.Cocona_fruit, count);
                    ItemSpawner.SpawnItem(Enums.ItemID.monstera_deliciosa_fruit, count);
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;

                case "root":
                    ItemSpawner.SpawnItem(Enums.ItemID.Malanga_bulb, count);
                    ItemSpawner.SpawnItem(Enums.ItemID.Cassava_bulb, count);
                    result.CmdExecResult = CmdExecResult.Executed;
                    break;

                default:
                    result.OutputAdd($"Unknown parameter name '{cmd.GetFirstArgumentName()}'");
                    result.CmdExecResult = CmdExecResult.Executed|CmdExecResult.Error; 
                    return result;
            }

            result.OutputAdd($"Spawn {cmd.GetFirstArgumentName()} {count} times.");
            return result;
        }
    }
}
