using GHTweaks;
using GHTweaks.UI.Console;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Doorstop
{
    internal class Entrypoint
    {
        public static void Start()
        {
            Task.Run(() =>
            {
                LogWriter.Write("Delete old log files...", LogType.Info);
                TryDeleteLogFiles();

                if (AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp") == null)
                {
                    try
                    {
                        LogWriter.Write("Assembly-CSharp.dll already loaded, apply patches.", LogType.Info);
                        Mod.Instance.ApplyPatches();
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Write(ex);
                    }
                }
                else
                {
                    LogWriter.Write("Waiting for Assembly-CSharp.dll", LogType.Info);
                    AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
                }
            });

        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            try
            {
                // For some reason there is no event raised when the Assembly-CSharp is loaded.
                if (AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp") != null)
                {
                    AppDomain.CurrentDomain.AssemblyLoad -= CurrentDomain_AssemblyLoad;
                    Mod.Instance.ApplyPatches();
                }
            }
            catch (Exception ex) 
            {
                LogWriter.Write(ex);
            }
        }

        /// <summary>
        /// Delete the GHTweaks.log, Harmony.log and doorstop_*.log from the last session.
        /// </summary>
        /// <returns>True if no exception occurred, otherwise false.</returns>
        private static bool TryDeleteLogFiles()
        {
            try
            {
                if (File.Exists(StaticFileNames.LogFileName))
                {
                    LogWriter.Write($"Delete '{Path.GetFileName(StaticFileNames.LogFileName)}' file.", LogType.Info);
                    File.Delete(StaticFileNames.LogFileName);
                }

                if (File.Exists(StaticFileNames.HarmonyLogFileName))
                {
                    LogWriter.Write($"Delete '{Path.GetFileName(StaticFileNames.HarmonyLogFileName)}' file.", LogType.Info);
                    File.Delete(StaticFileNames.HarmonyLogFileName);
                }

                var startTime = DateTime.Now.Subtract(new TimeSpan(0, 1, 0));
                var currentDirectory = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                var parentDirectory = currentDirectory.Parent;
                var doorstopLogFiles = parentDirectory.GetFiles("doorstop_*.log", SearchOption.TopDirectoryOnly);
                foreach (var logFile in doorstopLogFiles)
                {
                    if (logFile.LastWriteTime < startTime)
                    {
                        try
                        {
                            LogWriter.Write($"Delete '{logFile.Name}' file.", LogType.Info);
                            logFile.Delete();
                        }
                        catch (Exception ex)
                        {
                            ConsoleWindow.WriteLine(ex.Message, Style.TextColor.Error);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ConsoleWindow.WriteLine(ex.Message, Style.TextColor.Error);
                LogWriter.Write(ex.ToString(), LogType.Exception);
            }
            return false;
        }
    }
}
