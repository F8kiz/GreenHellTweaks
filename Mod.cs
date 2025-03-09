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
using GHTweaks.UI.Console;

namespace GHTweaks
{
    public partial class Mod
    {
        /// <summary>
        /// Get the GHTweaks mod instance.
        /// </summary>
        public static Mod Instance
        {
            get => instance ??= new Mod();
        }
        private static Mod instance;

        /// <summary>
        /// Get the GHTweaks mod version.
        /// </summary>
        public Version Version { get; private set; }

        /// <summary>
        /// Get the GHTweaks mod config.
        /// </summary>
        public Config Config { get; private set; }

        public bool IsPatchesApplied { get; private set; }

        private readonly Harmony harmony;


        /// <summary>
        /// Private constructor.
        /// Provides single ton implementation.
        /// </summary>
        private Mod()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) 
                => LogWriter.Write((e.ExceptionObject as Exception)?.ToString() ?? "AppDomain.CurrentDomain.UnhandledException invalid ExceptionObject", LogType.Exception);
            try
            {
                var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(StaticFileNames.ExecutingAssembly);
                Version = new Version(versionInfo.FileVersion);

                if (!TryLoadConfig() && Config == null)
                {
                    LogWriter.Write($"Initialize new Config...", LogType.Info);
                    Config = new Config();

                    if (!File.Exists(StaticFileNames.ModConfigFileName) && TrySaveConfig())
                        LogWriter.Write($"Created new file '{StaticFileNames.ModConfigFileName}.", LogType.Info);
                }
                LogWriter.SetWriteDebugLogs(Config.DebugModeEnabled);

                LogWriter.Write($"SetEnvironmentVariable(\"HARMONY_LOG_FILE\", {StaticFileNames.HarmonyLogFileName}), and initialize...");
                Environment.SetEnvironmentVariable("HARMONY_LOG_FILE", StaticFileNames.HarmonyLogFileName);

                harmony = new Harmony("de.fakiz.gh.tweaks");

                LogWriter.Write($"Harmony initialized: {harmony != null}");
                LogWriter.Write($"Subscribe P2PTransportLayer events...");

                //P2PTransportLayer.OnLobbyEnterEvent += (value) => P2PTransportLayerEventHandler();
                //P2PTransportLayer.OnSessionConnectStartEvent += () => P2PTransportLayerEventHandler();
                //LogWriter.Write($"P2PTransportLayer events subscribed.");
#if DEBUG
                if (!Config.ConsumeKeyStrokes)
                    Config.ConsumeKeyStrokes = true;
                
                if (!Config.DebugModeEnabled)
                    Config.DebugModeEnabled = true;

                if (!Config.SkipIntro)
                    Config.SkipIntro = true;
#endif
            }
            catch(Exception ex)
            {
                LogWriter.Write(ex);
            }
        }


        public void ApplyPatches()
        {
            try
            {
                if (IsPatchesApplied)
                    return;
                
                LogWriter.Write("Apply patches...");
                Harmony.DEBUG = Config.DebugModeEnabled;

                //harmony.PatchAll(Assembly.GetExecutingAssembly());
                var assembly = Assembly.GetExecutingAssembly();

                if (harmony.GetPatchedMethods().Count() < 1)
                    harmony.PatchCategory(assembly, PatchCategory.Required);
                
                harmony.PatchCategory(assembly, PatchCategory.Default);
#if DEBUG
                harmony.PatchCategory(assembly, PatchCategory.AIManager);
#endif
                // Patch everything that is categorized 
                var propertyInfos = Config.GetType().GetProperties();
                foreach (var info in propertyInfos) 
                {
                    var attribute = info.GetCustomAttribute<PatchCategoryAttribute>();
                    if (attribute == null)
                        continue;
                    
                    if (!(info.GetValue(Config) is IPatchConfig config))
                    {
                        LogWriter.Write($"Config.{info.Name} does not implement IPatchConfig interface", LogType.Error);
                        continue;
                    }

                    if (config.HasAtLeastOneEnabledPatch)
                    {
                        Console.WriteLine($"PatchCategory.{attribute.PatchCategory}");

                        LogWriter.Write($"PatchCategory.{attribute.PatchCategory}");
                        harmony.PatchCategory(assembly, attribute.PatchCategory);
                    }
                }

                if (Config.HasAtLeastOneChangedAIParamConfig())
                {
                    LogWriter.Write($"PatchCategory.{PatchCategory.AIManager}");
                    harmony.PatchCategory(assembly, PatchCategory.AIManager);
                }
                if (Config.ConsumeKeyStrokes)
                {
                    LogWriter.Write($"PatchCategory.{PatchCategory.GreenHellGameUpdate}");
                    harmony.PatchCategory(assembly, PatchCategory.GreenHellGameUpdate);
                }

                IsPatchesApplied = true;
                LogWriter.Write("Patches applied...");
            }
            catch (Exception ex)
            {
                ConsoleWindow.WriteLine(ex);
            }
        }

        public void RemovePatches() => RemovePatches(false);

        public void UnloadMod()
        {
            LogWriter.Write("Unload mod...", LogType.Info);
            RemovePatches(true);
            ConsoleWindow.Destroy();
            instance = null;

            LogWriter.Write("Mod unloaded.", LogType.Info);
            GC.Collect();
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
        /// Saves the current config.
        /// </summary>
        /// <returns>True if the config was saved successfully, otherwise false.</returns>
        public bool TrySaveConfig()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using StreamWriter sw = new StreamWriter(StaticFileNames.ModConfigFileName, false);
                serializer.Serialize(sw, Config);

                return true;
            }
            catch (Exception ex)
            {
                ConsoleWindow.WriteLine(ex);
            }
            return false;
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
                    LogWriter.Write("Failed to get HUDMessage instance", LogType.Error);
                else
                    hudMessages.AddMessage(message, color, HUDMessageIcon.None, "", null);

                ConsoleWindow.WriteLine(message);
            }
            catch (Exception ex)
            {
                LogWriter.Write("Failed to print message, exception: " + ex.Message, LogType.Debug);
                ConsoleWindow.WriteLine(ex.Message, Style.TextColor.Error);
            }
        }

        public void PauseGame(bool pause)
        {
            try
            {
                var player = Player.Get();
                if (player != null && GreenHellGame.Instance.m_LoadGameState == LoadGameState.None)
                {
                    if (pause)
                    {
                        //PostProcessManager.Get().SetWeight(PostProcessManager.Effect.InGameMenu, 1f);
                        CameraManager.Get().OutlineCameraToggle(false, null);
                        player.BlockMoves();
                        player.BlockRotation();
                        CursorManager.Get().ShowCursor(true, false);
                        MenuInGameManager.Get().m_CursorVisible = true;
                        MainLevel.Instance.Pause(true);
                    }
                    else
                    {
                        player.UnblockMoves();
                        player.UnblockRotation();
                        if (MenuInGameManager.Get().m_CursorVisible)
                        {
                            CursorManager.Get().ShowCursor(false, false);
                            MenuInGameManager.Get().m_CursorVisible = false;
                        }
                        CameraManager.Get().OutlineCameraToggle(true, null);
                        //PostProcessManager.Get().SetWeight(PostProcessManager.Effect.InGameMenu, 0f);
                        MainLevel.Instance.Pause(false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex);
            }
        }


        private void RemovePatches(bool includeRequiredPatches)
        {
            try
            {
                if (!IsPatchesApplied)
                    return;

                LogWriter.Write("Remove patches...");
                harmony.UnpatchAll("de.fakiz.gh.tweaks");

                if (!includeRequiredPatches)
                    harmony.PatchCategory(Assembly.GetExecutingAssembly(), PatchCategory.Required);

                IsPatchesApplied = false;
            }
            catch (Exception ex)
            {
                ConsoleWindow.WriteLine(ex);
            }
        }

        //private void P2PTransportLayerEventHandler()
        //{
        //    if (P2PSession.Instance.GetGameVisibility() != P2PGameVisibility.Singleplayer)
        //        RemovePatches();
        //    else
        //        ApplyPatches();
        //}

        /// <summary>
        /// Saves the current player location inside the configuration file.
        /// </summary>
        private void SaveCurrentPlayerPosition()
        {
            Vector3 worldPosition = Player.Get().GetWorldPosition();
            Config.PlayerHomePosition = new SerializableVector3(worldPosition.x, worldPosition.y, worldPosition.z);
            LogWriter.Write(string.Format("Current player position: {0}", Config.PlayerHomePosition));
            if (!TrySaveConfig())
            {
                PrintMessage("Failed to save ModConfig.xml", LogType.Error);
                return;
            }
            PrintMessage("Saved new PlayerHomePosition", LogType.Info);
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
                if (!File.Exists(StaticFileNames.ModConfigFileName))
                {
                    LogWriter.Write($"Found no config file, FilePath: {StaticFileNames.ModConfigFileName}", LogType.Error);
                    return false;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (FileStream fs = new FileStream(StaticFileNames.ModConfigFileName, FileMode.Open))
                    cfg = serializer.Deserialize(fs) as Config;

                Config = cfg;
            }
            catch (Exception ex)
            {
                ConsoleWindow.WriteLine(ex);
                return false;
            }

            if (cfg == null)
            {
                LogWriter.Write($"Failed to load '{StaticFileNames.ModConfigFileName}' file!", LogType.Error);
                return false;
            }

            Cheats.m_OneShotConstructions = Config.ConstructionConfig.OneShotConstructions;
            Cheats.m_InstantBuild = Config.ConstructionConfig.InstantBuild;
            return true;
        }
    }
}
