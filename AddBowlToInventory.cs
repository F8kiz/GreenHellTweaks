using GHTweaks.UI.Console;

using HarmonyLib;

using System.Reflection;

namespace GHTweaks
{
    internal static class SharedProperties
    {
        public const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

        public static bool IsOnAddToInventory
        {
            get => isOnAddToInventory;
            set
            {
                if (isOnAddToInventory == value)
                    return;

                isOnAddToInventory = value;

                var msg = $"Set IsOnAddToInventory: {value}";
                ConsoleWindow.WriteLine(msg);
                LogWriter.Write(msg);
            }
        }
        private static bool isOnAddToInventory = false;
    }

    [HarmonyPatchCategory(PatchCategory.AddBowlToInventory)]
    [HarmonyPatch(typeof(Bowl), nameof(Bowl.OnAddToInventory))]
    internal class BowlOnAddToInventory
    {
        static void Prefix(Bowl __instance) => SharedProperties.IsOnAddToInventory = true;
        

        static void Postfix(Bowl __instance)
        {
            if (!SharedProperties.IsOnAddToInventory)
                return;

            var mi = typeof(Bowl).GetMethod("SetState", SharedProperties.bindingFlags);
            mi.Invoke(__instance, new object[] { Bowl.State.HerbCooked });
            LogWriter.Write($"BowlOnAddToInventory.Postfix: Bowl.State: {__instance.m_State}");

            SharedProperties.IsOnAddToInventory = false;
        }
    }

    [HarmonyPatchCategory(PatchCategory.AddBowlToInventory)]
    [HarmonyPatch(typeof(Bowl), nameof(Bowl.Spill))]
    internal class BowlSpill
    {
        static bool Prefix(Bowl __instance) => !SharedProperties.IsOnAddToInventory;
    }
}
