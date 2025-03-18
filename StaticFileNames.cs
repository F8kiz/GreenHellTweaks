using System.IO;
using System.Reflection;

namespace GHTweaks
{
    internal class StaticFileNames
    {
        public static readonly string ExecutingAssembly;
        public static readonly string ModConfigFileName;
        public static readonly string LogFileName;
        public static readonly string HarmonyLogFileName;
        public static readonly string TypeSuggestionsFileName;


        static StaticFileNames()
        {
            ExecutingAssembly = Assembly.GetExecutingAssembly().Location;

            string rootDir = Path.GetDirectoryName(ExecutingAssembly);
            ModConfigFileName = Path.Combine(rootDir, "GHTweaksConfig.xml");
            LogFileName = Path.Combine(rootDir, "GHTweaks.log");
            HarmonyLogFileName = Path.Combine(rootDir, "harmony.log");
            TypeSuggestionsFileName = Path.Combine(rootDir, "TypeSuggestions.txt");

            var buffer = new string[] {
                "StaticFileNames:",
                $" - ExecutingAssembly: {ExecutingAssembly}",
                $" - ModConfigFileName: {ModConfigFileName}",
                $" - LogFileName: {LogFileName}",
                $" - HarmonyLogFileName: {HarmonyLogFileName}"
            };
            LogWriter.Write(string.Join("\n", buffer), LogType.Info);
        }
    }
}
