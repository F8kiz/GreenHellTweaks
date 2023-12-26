using GHTweaks.Serializable;

using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), "m_HP", MethodType.Setter)]
    internal class PlayerConditionModuleHP
    {
        static void Prefix(ref float value)
        {
            PlayerConditionModuleConfig config = Mod.Instance.Config.PlayerConditionModuleConfig;
            if (config.MinHealthPoints > 0 && value < config.MinHealthPoints)
                value = config.MinHealthPoints;
        }
    }
}
