using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(LiquidContainerInfo), "LoadParams")]
    internal class LiquidContainerInfoCapacity
    {
        static void Postfix(LiquidContainerInfo __instance)
        {
            if (Mod.Instance.Config.LiquidContainerInfoConfig.MinCapacity < 0)
                return;

            if (__instance.m_LiquidType == Enums.LiquidType.Water || __instance.m_LiquidType == Enums.LiquidType.DirtyWater || 
                __instance.m_LiquidType == Enums.LiquidType.PoisonedWater || __instance.m_LiquidType == Enums.LiquidType.PoisonedWater)
                __instance.m_Capacity = Mod.Instance.Config.LiquidContainerInfoConfig.MinCapacity;
        }
    }
}
