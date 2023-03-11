/*
// Injection point
// ===============

public MainMenu() : base()
{
    if (!GHTweaks.Mod.Instance.Config.SkipIntro)
    {
        this.m_FadeInDuration = 1f;
        this.m_FadeOutDuration = 1.7f;
        this.m_FadeOutSceneDuration = 3f;
        this.m_CompanyLogoDuration = 5f;
        this.m_GameLogoDuration = 7f;
        this.m_BlackScreenDuration = 1f;
    }
    else
    {
        this.m_FadeInDuration = 0f;
        this.m_FadeOutDuration = 0f;
        this.m_FadeOutSceneDuration = 0f;
        this.m_CompanyLogoDuration = 0f;
        this.m_GameLogoDuration = 0f;
        this.m_BlackScreenDuration = 0f;
    }
    this.m_StateStartTime = -1f;
    this.m_ButtonActivationTime = -1f;
    this.m_ButtonsFadeInDuration = 2f;
    this.m_EarlyAccess = (GreenHellGame.s_GameVersion < GreenHellGame.s_GameVersionEarlyAccessUpdate13);
    this.m_GameVersionInitialPosLocal = Vector3.zero;
}
*/
using System.IO;
using System;
using System.Reflection;
using HarmonyLib;
using System.Xml.Serialization;
using GreenHellTweaks.Serializable;
using UnityEngine;
using System.Collections.Generic;

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
        public Version Version { get; private set; } = new Version(2, 0, 0, 0);

        /// <summary>
        /// Get the GHTweaks mod config.
        /// </summary>
        public Config Config { get; private set; }


        /// <summary>
        /// Get or set the GHTweaksConfig.xml file path.
        /// </summary>
        private readonly string strModConfigFileName;

        /// <summary>
        /// Get or set the GHTweaks.log file path.
        /// </summary>
        private readonly string strLogFileName;


        /// <summary>
        /// Private constructor.
        /// Provides single ton implementation.
        /// </summary>
        private Mod()
        {
            string rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            strModConfigFileName = Path.Combine(rootDir, "GHTweaksConfig.xml");
            strLogFileName = Path.Combine(rootDir, "GHTweaks.log");

            if (File.Exists(strLogFileName))
                File.Delete(strLogFileName);

            string harmonyLogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "harmony.log.txt");
            if (File.Exists(harmonyLogFile))
                File.Delete(harmonyLogFile);

            TryLoadConfig();

            try
            {
                WriteLog("Apply patches...");
                Harmony.DEBUG = true;
                Harmony harmony = new Harmony("de.fakiz.gh.tweaks");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                
                WriteLog("Check patches...");
                IEnumerable<MethodBase> methods = harmony.GetPatchedMethods();
                foreach(MethodBase mb in methods)
                    WriteLog($"Patched Method '{mb.ReflectedType.Name}.{mb.Name}'");
            }
            catch (Exception ex) 
            {
                WriteLog(ex.ToString(), LogType.Exception);
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
                if (logType == LogType.Debug && !Config.DebugModeEnabled)
                    return;

                using (StreamWriter sw = new StreamWriter(strLogFileName, true))
                {
                    sw.WriteLine(string.Format("[{0:HH:mm:ss}][{1}] {2}", DateTime.Now, logType, message));
                }
            }
            catch (System.Exception) { }
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
            catch (System.Exception ex)
            {
                WriteLog(ex.Message, LogType.Exception);
                return false;
            }

            if (cfg == null)
            {
                Config = new Config();
                WriteLog("Failed to load config.", LogType.Error);
                return false;
            }

            Cheats.m_OneShotConstructions = Config.ConstructionConfig.OneShotConstructions;
            Cheats.m_InstantBuild = Config.ConstructionConfig.InstantBuild;
            return true;
        }


    }
}
