namespace GHTweaks.UI.Console.Command.Core
{
    public interface ICommand
    {
        string[] UsageExamples { get; }

        CommandResult Execute(CommandInfo cmd);
    }
}