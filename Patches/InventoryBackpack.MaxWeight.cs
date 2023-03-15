using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(InventoryBackpack), "Awake")]
    internal class InventoryBackpackMaxWeight
    {
        static void Prefix(InventoryBackpack __instance)
        {
            if (Mod.Instance.Config.InventoryBackpackConfig.MaxWeight > 0)
                __instance.m_MaxWeight = Mod.Instance.Config.InventoryBackpackConfig.MaxWeight;
        }
    }
}
