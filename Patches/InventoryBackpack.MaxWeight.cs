using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(InventoryBackpack), ".ctor", MethodType.Constructor)]
    internal class InventoryBackpackMaxWeight
    {
        static void Postfix(InventoryBackpack __instance)
        {
            if (Mod.Instance.Config.InventoryBackpackConfig.MaxWeight > 0)
            {
                AccessTools.FieldRef<InventoryBackpack, float> maxWeight = AccessTools.FieldRefAccess<InventoryBackpack, float>("m_MaxWeight");
                maxWeight(__instance) = Mod.Instance.Config.InventoryBackpackConfig.MaxWeight;
            }
        }
    }
}
