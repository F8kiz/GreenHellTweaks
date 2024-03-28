using GHTweaks.Configuration;
using System.Reflection;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(PlayerConditionModule), nameof(PlayerConditionModule.m_Stamina), MethodType.Setter)]
    internal class PlayerConditionModuleStamina
    {
        static void Prefix(PlayerConditionModule __instance, ref float value)
        {
            FieldInfo fiMaxStamina = AccessTools.Field(typeof(PlayerConditionModule), "m_MaxStamina");
            float maxStamina = (float)fiMaxStamina.GetValue(__instance);
            if (value == maxStamina)
                return;

            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.StaminaConsumptionThreshold > 0)
            {
                if (__instance.m_Stamina - value > config.StaminaConsumptionThreshold)
                    value = __instance.m_Stamina - config.StaminaConsumptionThreshold;
            }
        }
    }
}
