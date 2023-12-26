using GHTweaks;

using System;
using System.Linq;

namespace Doorstop
{
    internal class Entrypoint
    {
        public static void Start()
        {
            Mod.Instance.WriteLog("Waiting for Assembly-CSharp.dll", LogType.Debug);
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            // For some reason there is no event raised when the Assembly-CSharp is loaded.
            if (AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp") != null)
            {
                AppDomain.CurrentDomain.AssemblyLoad -= CurrentDomain_AssemblyLoad;
                Mod.Instance.ApplyPatches();
            }
        }
    }
}
