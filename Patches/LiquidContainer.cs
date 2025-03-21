﻿using Enums;
using HarmonyLib;
using System;
using System.Collections.Generic;

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

            // Tuple<float, bool>
            // Item1: LiquidContainer.LiquidContainerInfo.m_Capacity property value.
            // Item2: If true, the property value LiquidContainer.LiquidContainerInfo.m_Amount should also be set.
            var capacityMap = new Dictionary<ItemID, Tuple<float, bool>>()
            {
                { ItemID.Bidon, new Tuple<float, bool>(config.BidonCapacity, false) },
                { ItemID.clay_bidon, new Tuple<float, bool>(config.ClayBidonCapacity, false) },
                { ItemID.clay_bowl_big, new Tuple<float, bool>(config.ClayBowlBigCapacity, false) },
                { ItemID.clay_bowl_small, new Tuple<float, bool>(config.ClayBowlSmallCapacity, false) },
                { ItemID.Coconut, new Tuple<float, bool>(config.CoconutCapacity, true) },
                { ItemID.Coconut_Bidon, new Tuple<float, bool>(config.CoconutBidonCapacity, false) },
                { ItemID.Coconut_Bowl, new Tuple<float, bool>(config.CoconutBowlCapacity, false) },
                { ItemID.Pot, new Tuple<float, bool>(config.PotCapacity, false) },
                { ItemID.Turtle_shell, new Tuple<float, bool>(config.TurtleShellCapacity, false) }
            };

            if (capacityMap.TryGetValue(id, out Tuple<float, bool> capacity))
                SetCapacity(ref __instance, capacity.Item1, capacity.Item2);
            else
                LogWriter.Write($"LiquidContainer.SetupInfo: Not supported LiquidContainer: {id}, capacity: {__instance.m_LCInfo.m_Capacity}", LogType.Info);   
        }

        static void SetCapacity(ref LiquidContainer instance, float value, bool setAmountToo)
        {
#if DEBUG
            LogWriter.Write($"LiquidContainer.SetupInfo (init values):    {instance.m_LCInfo.m_ID}, capacity: {instance.m_LCInfo.m_Capacity}, amount: {instance.m_LCInfo.m_Amount}");
#endif
            if (value > 0)
            {
                instance.m_LCInfo.m_Capacity = value;
                if (setAmountToo)
                    instance.m_LCInfo.m_Amount = value;
#if DEBUG
                LogWriter.Write($"LiquidContainer.SetupInfo (updated values): {instance.m_LCInfo.m_ID}, capacity: {instance.m_LCInfo.m_Capacity}, amount: {instance.m_LCInfo.m_Amount}");
#endif
            }
        }
    }
}
