using System.IO;
using System;
using System.Reflection;
using HarmonyLib;
using System.Xml.Serialization;
using UnityEngine;
using GHTweaks.Configuration;
using GHTweaks.Models;
using System.Linq;
using GHTweaks.Patches;
using GHTweaks.Configuration.Core;

namespace GHTweaks
{
    public partial class Mod
    {
        /// <summary>
        /// Get the GHTweaks mod instance.
        /// </summary>
        public static Mod Instance
        {
            get => instance ?? (instance = new Mod());
        }
        private static Mod instance;

        /// <summary>
        /// Get the GHTweaks mod version.
        /// </summary>
        public Version Version { get; private set; } = new Version(2, 15, 2, 0);

        /// <summary>
        /// Get the GHTweaks mod config.
        /// </summary>
        public Config Config { get; private set; }

        public bool IsPatchesApplied { get; private set; }

        /// <summary>
        /// Get the GHTweaksConfig.xml file path.
        /// </summary>
        private readonly string strModConfigFileName;

        /// <summary>
        /// Get the GHTweaks.log file path.
        /// </summary>
        private readonly string strLogFileName;

        /// <summary>
        /// Get the Harmony.log file path.
        /// </summary>
        private readonly string strHarmonyLogFileName;

        private readonly Harmony harmony;


        /// <summary>
        /// Private constructor.
        /// Provides single ton implementation.
        /// </summary>
        private Mod()
        {
            string rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            strModConfigFileName = Path.Combine(rootDir, "GHTweaksConfig.xml");
            strLogFileName = Path.Combine(rootDir, "GHTweaks.log");
            strHarmonyLogFileName = Path.Combine(rootDir, "harmony.log");

            AppDomain.CurrentDomain.UnhandledException += (s, e) => WriteLog((e.ExceptionObject as Exception)?.ToString() ?? "AppDomain.CurrentDomain.UnhandledException invalid ExceptionObject", LogType.Exception);

            WriteLog($"SetEnvironmentVariable(\"HARMONY_LOG_FILE\", {strHarmonyLogFileName})", LogType.Debug);
            Environment.SetEnvironmentVariable("HARMONY_LOG_FILE", strHarmonyLogFileName);

            harmony = new Harmony("de.fakiz.gh.tweaks");

            TryDeleteLogFiles();
            if (!TryLoadConfig() && Config == null)
            {
                WriteLog($"Initialize new Config...", LogType.Info);
                Config = new Config();

                if (!File.Exists(strModConfigFileName) && TrySaveConfig())
                    WriteLog("Created new file '{strModConfigFileName}.");
            }

            P2PTransportLayer.OnLobbyEnterEvent += (value) =>
            {
                if (P2PSession.Instance.GetGameVisibility() != P2PGameVisibility.Singleplayer)
                    RemovePatches();
                else 
                    ApplyPatches();
            };
            P2PTransportLayer.OnSessionConnectStartEvent += () =>
            {
                if (P2PSession.Instance.GetGameVisibility() != P2PGameVisibility.Singleplayer)
                    RemovePatches();
                else
                    ApplyPatches();
            };
        }


        public void ApplyPatches()
        {
            try
            {
                if (IsPatchesApplied)
                    return;
                
                WriteLog("Apply patches...");
                Harmony.DEBUG = Config.DebugModeEnabled;

                //harmony.PatchAll(Assembly.GetExecutingAssembly());
                var assembly = Assembly.GetExecutingAssembly();

                if (harmony.GetPatchedMethods().Count() < 1)
                    harmony.PatchCategory(assembly, PatchCategory.Required);
                
                harmony.PatchCategory(assembly, PatchCategory.Default);

                // Patch everything which is categorized 
                var propertyInfos = Config.GetType().GetProperties();
                foreach (var info in propertyInfos) 
                {
                    var attribute = info.GetCustomAttribute<PatchCategoryAttribute>();
                    if (attribute == null)
                        continue;
                    
                    if (!(info.GetValue(Config) is IPatchConfig config))
                    {
                        WriteLog($"Config.{info.Name} does not implement IPatchConfig interface", LogType.Error);
                        continue;
                    }

                    if (config.HasAtLeastOneEnabledPatch)
                    {
                        WriteLog($"PatchCategory.{attribute.PatchCategory}");
                        harmony.PatchCategory(assembly, attribute.PatchCategory);
                    }
                }

                if (Config.ConsumeKeyStrokes)
                {
                    WriteLog($"PatchCategory.{PatchCategory.GreenHellGameUpdate}");
                    harmony.PatchCategory(assembly, PatchCategory.GreenHellGameUpdate);
                }
                if (Config.CheatsEnabled)
                {
                    WriteLog($"PatchCategory.{PatchCategory.Cheats}");
                    harmony.PatchCategory(assembly, PatchCategory.Cheats);
                }

                //WriteLog("Patched methods:");
                //IEnumerable<MethodBase> methods = harmony.GetPatchedMethods();
                //foreach (MethodBase mb in methods)
                //    WriteLog($"Patched Method '{mb.ReflectedType.Name}.{mb.Name}'");

                IsPatchesApplied = true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString(), LogType.Exception);
            }
        }

        public void RemovePatches()
        {
            try
            {
                if (!IsPatchesApplied)
                    return;
                
                WriteLog("Remove patches...");
                harmony.UnpatchAll("de.fakiz.gh.tweaks");
                harmony.PatchCategory(Assembly.GetExecutingAssembly(), PatchCategory.Required);

                IsPatchesApplied = false;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString(), LogType.Exception);
            }
        }

        public void ReloadConfig()
        {
            if (TryLoadConfig())
            {
                PlayerConditionModuleInitialize.UpdateFields(PlayerConditionModule.Get());
                PrintMessage("Config reloaded", LogType.Info);
            }
            else
            {
                PrintMessage("Failed to reload config!", LogType.Error);
            }
        }

        /// <summary>
        /// Print a text message on the screen.
        /// </summary>
        /// <param name="message">The message which should be printed on the screen.</param>
        /// <param name="logType">The log type of the message.</param>
        public void PrintMessage(string message, LogType logType = LogType.Debug)
        {
            try
            {
                Color? color = null;
                if (logType == LogType.Debug)
                    color = new Color?(new Color(115f, 0f, 230f));
                else if (logType == LogType.Error)
                    color = new Color?(new Color(230f, 115f, 0f));
                else if (logType == LogType.Exception)
                    color = new Color?(new Color(230f, 0f, 0f));

                HUDManager hudManager = HUDManager.Get();
                HUDMessages hudMessages = (hudManager?.GetHUD(typeof(HUDMessages))) as HUDMessages;

                if (hudMessages == null)
                    WriteLog("Failed to get HUDMessage instance", LogType.Error);
                else
                    hudMessages.AddMessage(message, color, HUDMessageIcon.None, "", null);
            }
            catch (System.Exception ex)
            {
                WriteLog("Failed to print message, exception: " + ex.Message, LogType.Debug);
            }
        }

        /// <summary>
        /// Write a log message to the GHTweaks.log file.
        /// </summary>
        /// <param name="message">The message to write to the log file.</param>
        /// <param name="logType">The type of the log message.</param>
        public void WriteLog(string message, LogType logType=LogType.Debug)
        {
            try
            {
                string msg = string.Format("[{0:HH:mm:ss}][{1}] {2}", DateTime.Now, logType, message);
                CJDebug.m_Log += $"[{logType}] {message}\n";

                if (logType == LogType.Debug && !Config.DebugModeEnabled)
                    return;
                
                using (StreamWriter sw = new StreamWriter(strLogFileName, true))
                    sw.WriteLine(msg);
            }
            catch (Exception) { }
        }



        /// <summary>
        /// Saves the current player location inside the configuration file.
        /// </summary>
        private void SaveCurrentPlayerPosition()
        {
            Vector3 worldPosition = Player.Get().GetWorldPosition();
            Config.PlayerHomePosition = new SerializableVector3(worldPosition.x, worldPosition.y, worldPosition.z);
            WriteLog(string.Format("Current player position: {0}", Config.PlayerHomePosition));
            if (!TrySaveConfig())
            {
                PrintMessage("Failed to save ModConfig.xml", LogType.Error);
                return;
            }
            PrintMessage("Saved new PlayerHomePosition", LogType.Info);
        }

        /// <summary>
        /// Saves the current config.
        /// </summary>
        /// <returns>True if the config was saved successfully, otherwise false.</returns>
        private bool TrySaveConfig()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (StreamWriter sw = new StreamWriter(strModConfigFileName, false))
                    serializer.Serialize(sw, Config);

                return true;
            }
            catch (System.Exception ex)
            {
                WriteLog($"Failed to save config file! Exception: {ex.Message}", LogType.Exception);
            }
            return false;
        }

        /// <summary>
        /// Loads the GHTweaksConfig.xml file.
        /// </summary>
        /// <param name="cfg">Config instance</param>
        /// <returns>True if the config was loaded successfully, otherwise false.</returns>
        private bool TryLoadConfig()
        {
            Config cfg = null;
            try
            {
                if (!File.Exists(strModConfigFileName))
                {
                    WriteLog($"Found no config file, FilePath: {strLogFileName}", LogType.Error);
                    return false;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (FileStream fs = new FileStream(strModConfigFileName, FileMode.Open))
                    cfg = serializer.Deserialize(fs) as Config;

                Config = cfg;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Exception);
                return false;
            }

            if (cfg == null)
            {
                WriteLog($"Failed to load '{strModConfigFileName}' file!", LogType.Error);
                return false;
            }

            Cheats.m_OneShotConstructions = Config.ConstructionConfig.OneShotConstructions;
            Cheats.m_InstantBuild = Config.ConstructionConfig.InstantBuild;
            return true;
        }

        /// <summary>
        /// Delete the GHTweaks.log, Harmony.log and doorstop_*.log from the last session.
        /// </summary>
        /// <returns>True if no exception occurred, otherwise false.</returns>
        private bool TryDeleteLogFiles()
        {
            try
            {
                if (File.Exists(strLogFileName))
                {
                    WriteLog($"Delete '{Path.GetFileName(strLogFileName)}' file.", LogType.Info);
                    File.Delete(strLogFileName);
                }

                if (File.Exists(strHarmonyLogFileName))
                {
                    WriteLog($"Delete '{Path.GetFileName(strHarmonyLogFileName)}' file.", LogType.Info);
                    File.Delete(strHarmonyLogFileName);
                }

                var startTime = DateTime.Now.Subtract(new TimeSpan(0,1,0));
                var currentDirectory = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                var parentDirectory = currentDirectory.Parent;
                var doorstopLogFiles = parentDirectory.GetFiles("doorstop_*.log", SearchOption.TopDirectoryOnly);
                foreach(var logFile in doorstopLogFiles)
                {
                    if (logFile.LastWriteTime < startTime)
                    {
                        try
                        {
                            WriteLog($"Delete '{logFile.Name}' file.", LogType.Info);
                            logFile.Delete();
                        }
                        catch (Exception) { }
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                WriteLog(ex.ToString(), LogType.Exception);
            }
            return false;
        }

    }
}
