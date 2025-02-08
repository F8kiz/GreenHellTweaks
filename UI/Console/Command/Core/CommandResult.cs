using System.Collections.Generic;
using System.Linq;

namespace GHTweaks.UI.Console.Command.Core
{
    internal class CommandResult
    {
        public CommandInfo Command { get; private set; }

        public CmdExecResult CmdExecResult { get; set; } = CmdExecResult.NotExecuted;

        public List<ConsoleLine> Output { get; set; } = null;

        public ConsoleAction ConsoleAction { get; set; } = ConsoleAction.None;

        public object Value { get; set; } = null;


        public CommandResult(CommandInfo cmd) => Command = cmd;

        public CommandResult(CmdExecResult result) => CmdExecResult = result;

        public CommandResult(CommandInfo cmd, CmdExecResult result)
        {
            Command = cmd;
            CmdExecResult = result;
        }


        public void OutputAdd(ConsoleLine msg)
        {
            Output ??= new List<ConsoleLine>();
            Output.Add(msg);
        }

        public void OutputAdd(string msg, string color = null) => OutputAdd(new ConsoleLine(msg, color));

        public void OutputAddRange(List<string> messages, string color = null)
        {
            if (messages == null)
                return;

            Output ??= new List<ConsoleLine>();

            var lines = messages.Select(x => new ConsoleLine(x, color)).ToList();
            Output.AddRange(lines);
        }


        public override string ToString()
        {
            var v = Value ?? "null";
            return $"{Command.RawInput} >> CmdExecResult: {CmdExecResult}, ConsoleAction: {ConsoleAction}, Value: {v}";
        }
    }
}
