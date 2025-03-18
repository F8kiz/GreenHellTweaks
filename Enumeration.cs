using System;

namespace GHTweaks
{
    [Flags]
    public enum LogType
    {
        Debug = 1,
        Info = 2,
        Error = 4,
        Exception = 8
    }

    [Flags]
    public enum CmdExecResult
    {
        None = 0,
        Executed = 12,
        NotExecuted = 24,
        // 48,
        // 96
        // 192
        // 384
        // 768
        Warning = 1536,
        Error = 3072, 
        Exception = 6144,
        // 12288,
        // 24576,
        // 49152
    }

    public enum ConsoleAction
    {
        None,
        Clear,
        Close,
        SetBufferSize,
        SetCommandSuggestion
    }
}
