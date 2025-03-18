using GHTweaks.UI.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace GHTweaks
{
    internal static class LogWriter
    {
        private static readonly object writeLock = new object();


        /// <summary>
        /// The Setter cause an exception for a unknown reason.
        /// <para>
        ///     Please use <see cref="LogWriter.SetWriteDebugLogs(bool)"/> to set this property.
        /// </para>
        /// </summary>
        public static bool WriteDebugLogs { get; private set; } = true;

        public static void SetWriteDebugLogs(bool value)
        {
            Write($"Set {nameof(WriteDebugLogs)}: {value}");

            WriteDebugLogs = value;

            Write($"{nameof(WriteDebugLogs)}: {value}");

        }

        /// <summary>
        /// Write a log message to the GHTweaks.log file.
        /// </summary>
        /// <param name="message">The message to write to the log file.</param>
        /// <param name="logType">The type of the log message.</param>
        [DebuggerStepThrough]
        public static void Write(string message, LogType logType = LogType.Debug, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
        {
            string callerFileName = "";
            try
            {
                if (logType == LogType.Debug && !WriteDebugLogs)
                    return;

                var strBuffer = new List<string>();
                if (!string.IsNullOrEmpty(callerFilePath))
                {
                    callerFileName = Path.GetFileNameWithoutExtension(callerFilePath);
                    strBuffer.Add(callerFileName);
                }

                if (!string.IsNullOrEmpty(callerMemberName))
                    strBuffer.Add(callerMemberName);

                lock (writeLock)
                {
                    using StreamWriter sw = new StreamWriter(StaticFileNames.LogFileName, true);
                    sw.WriteLine($"[{DateTime.Now:HH:mm:ss}][{string.Join(".", strBuffer)}][{logType}] {message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                ConsoleWindow.WriteLine($"{nameof(Write)}: {ex.Message}", Style.TextColor.Error);
            }
        }

        public static void Write(Exception ex, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
            => Write(ex.ToString(), LogType.Exception, callerFilePath, callerMemberName);
    }
}
