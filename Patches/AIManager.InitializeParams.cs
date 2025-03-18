using AIs;

using HarmonyLib;

using System.Linq;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.AIManager)]
    [HarmonyPatch(typeof(AIManager), "InitializeAIParams")]
    internal class AIManagerInitializeParams
    {
        static void Postfix(AIManager __instance)
        {
            foreach (var kvp in __instance.m_AIParamsMap)
            {
                //csv.Add(ConvertAIParamsToCSVLine(kvp.Key, kvp.Value));
                var id = (AI.AIID)kvp.Key;
                var cfg = Mod.Instance.Config.AIParameterConfigs.FirstOrDefault(x => x.ID == id);
                if (cfg == null)
                {
                    WriteLog($"Found no config for {id}");
                    continue;
                }

                if (cfg.Health > 0)
                {
                    kvp.Value.m_Health = cfg.Health;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_Health)}, value: {kvp.Value.m_Health}");
                }

                if (cfg.HealthRegeneration > 0)
                {
                    kvp.Value.m_HealthRegeneration = cfg.HealthRegeneration;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_HealthRegeneration)}, value: {kvp.Value.m_HealthRegeneration}");
                }

                if (cfg.AttackRange > 0)
                {
                    kvp.Value.m_AttackRange = cfg.AttackRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_AttackRange)}, value: {kvp.Value.m_AttackRange}");
                }

                if (cfg.Damage > 0)
                {
                    kvp.Value.m_Damage = cfg.Damage;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_Damage)}, value: {kvp.Value.m_Damage}");
                }

                if (cfg.JumpAttackRange > 0)
                {
                    kvp.Value.m_JumpAttackRange = cfg.JumpAttackRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_JumpAttackRange)}, value: {kvp.Value.m_JumpAttackRange}");
                }

                if (cfg.JumpBackRange > 0)
                {
                    kvp.Value.m_JumpBackRange = cfg.JumpBackRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_JumpBackRange)}, value: {kvp.Value.m_JumpBackRange}");
                }

                if (cfg.PoisonLevel > 0)
                {
                    kvp.Value.m_PoisonLevel = (int)cfg.PoisonLevel;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_PoisonLevel)}, value: {kvp.Value.m_PoisonLevel}");
                }

                if (cfg.MinBitingDuration > 0)
                {
                    kvp.Value.m_MinBitingDuration = cfg.MinBitingDuration;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_MinBitingDuration)}, value: {kvp.Value.m_MinBitingDuration}");
                }

                if (cfg.MaxBitingDuration > 0)
                {
                    kvp.Value.m_MaxBitingDuration = cfg.MaxBitingDuration;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_MaxBitingDuration)}, value: {kvp.Value.m_MaxBitingDuration}");
                }

                if (cfg.EnemySenseRange > 0)
                {
                    kvp.Value.m_EnemySenseRange = cfg.EnemySenseRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_EnemySenseRange)}, value: {kvp.Value.m_EnemySenseRange}");
                }

                if (cfg.SightAngle > 0)
                {
                    kvp.Value.m_SightAngle = cfg.SightAngle;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_SightAngle)}, value: {kvp.Value.m_SightAngle}");
                }

                if (cfg.SightRange > 0)
                {
                    kvp.Value.m_SightRange = cfg.SightRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_SightRange)}, value: {kvp.Value.m_SightRange}");
                }

                if (cfg.HearingSneakRange > 0)
                {
                    kvp.Value.m_HearingSneakRange = cfg.HearingSneakRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_HearingSneakRange)}, value: {kvp.Value.m_HearingSneakRange}");
                }

                if (cfg.HearingWalkRange > 0)
                {
                    kvp.Value.m_HearingWalkRange = cfg.HearingWalkRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_HearingWalkRange)}, value: {kvp.Value.m_HearingWalkRange}");
                }

                if (cfg.HearingRunRange > 0)
                {
                    kvp.Value.m_HearingRunRange = cfg.HearingRunRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_HearingRunRange)}, value: {kvp.Value.m_HearingRunRange}");
                }

                if (cfg.HearingSwimRange > 0)
                {
                    kvp.Value.m_HearingSwimRange = cfg.HearingSwimRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_HearingSwimRange)}, value: {kvp.Value.m_HearingSwimRange}");
                }

                if (cfg.HearingActionRange > 0)
                {
                    kvp.Value.m_HearingActionRange = cfg.HearingActionRange;
                    WriteLog($"Set {id}.{nameof(kvp.Value.m_HearingActionRange)}, value: {kvp.Value.m_HearingActionRange}");
                }
            }
        }

        static void WriteLog(string str)
        {
#if !DEBUG
            LogWriter.Write(str);
#endif
        }
    }
}
