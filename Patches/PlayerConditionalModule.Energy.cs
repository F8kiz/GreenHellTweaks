using GHTweaks.Configuration;
using HarmonyLib;

using System;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_Energy), MethodType.Setter)]
    internal class PlayerConditionalModuleEnergy
    {
        //static int updateCount = 0;

        static float lastConsumption = 0;


        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (value >= 100 || value > __instance.m_Energy || config.EnergyConsumptionPerInGameSeconds < 1)
                return;

            // Updates per second: 200 - 268
            // Max deceased: 0.007499695
            // Min deceased: 0.0005264282

            float currentTimeMinutes = MainLevel.Instance.GetCurrentTimeMinutes();
            float min = Math.Min(currentTimeMinutes, lastConsumption);
            float max = Math.Max(currentTimeMinutes, lastConsumption);
            float elapsedMinutes = max - min;

            if (elapsedMinutes < config.EnergyConsumptionPerInGameSeconds)
                value = __instance.m_Energy;
            else
                lastConsumption = MainLevel.Instance.GetCurrentTimeMinutes();
            
            //if (++updateCount % 500 == 0)
            //{
            //    Mod.Instance.WriteLog($"PlayerConditionalModule.m_Energy value: {value}, lastConsumption: {lastConsumption}, currentTimeMinutes: {currentTimeMinutes}, elapsedMinutes: {elapsedMinutes}");
            //    updateCount = 0;
            //}
        }
    }
}
