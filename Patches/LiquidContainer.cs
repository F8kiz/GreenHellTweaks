using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.LiquidContainer)]
    [HarmonyPatch(typeof(LiquidContainer), nameof(LiquidContainer.SetupInfo))]
    internal class LiquidContainerSetupInfo
    {
        static void Postfix(LiquidContainer __instance)
        {
            var id = __instance.m_LCInfo.m_ID;
            var config = Mod.Instance.Config.LiquidContainerConfig;
            var capacity = -1f;

            if (id == Enums.ItemID.Coconut_Bowl)
            {
                if (config.CoconutBowlCapacity > 0)
                    capacity = config.CoconutBowlCapacity;
            }
            else if (id == Enums.ItemID.Coconut_Bidon)
            {
                if (config.CoconutBidonCapacity > 0)
                    capacity = config.CoconutBidonCapacity;
            }
            else if (id == Enums.ItemID.Coconut)
            {
                if (config.CoconutCapacity > 0)
                {
                    __instance.m_LCInfo.m_Capacity = config.CoconutCapacity;
                    __instance.m_LCInfo.m_Amount = config.CoconutCapacity;
#if DEBUG
                    Mod.Instance.WriteLog($"LiquidContainer.SetupInfo: {__instance.m_LCInfo.m_ID}, capacity: {__instance.m_LCInfo.m_Capacity}, amount: {__instance.m_LCInfo.m_Amount}");
#endif
                    return;
                }
            }
            else if (id == Enums.ItemID.Bidon)
            {
                if (config.BidonCapacity > 0)
                    capacity = config.BidonCapacity;
            }
            else if (id == Enums.ItemID.Pot)
            {
                if (config.PotCapacity > 0)
                    capacity = config.PotCapacity;
            }
            else
            {
                Mod.Instance.WriteLog($"LiquidContainer.SetupInfo: Not supported LiquidContainer: {id}, capacity: {__instance.m_LCInfo.m_Capacity}", LogType.Info);
            }
            if (capacity > 0)
            {
                __instance.m_LCInfo.m_Capacity = capacity;
#if DEBUG
                Mod.Instance.WriteLog($"LiquidContainer.SetupInfo: {__instance.m_LCInfo.m_ID}, capacity: {__instance.m_LCInfo.m_Capacity}");
#endif
            }
        }
    }
}
