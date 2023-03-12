using HarmonyLib;

using static P2PStats.ReplicationStat;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(PlayerConditionModule), "m_HP", MethodType.Setter)]
    internal class PlayerConditionModuleHP
    {
        static void Postfix(ref float value)
        {
            if (ScenarioManager.Get().LoadingCompleted())
            {
                if (Mod.Instance.Config.PlayerConditionModuleConfig.MinHealthPoints > 0)
                    value = Mod.Instance.Config.PlayerConditionModuleConfig.MinHealthPoints;
            }
        }
    }
}
