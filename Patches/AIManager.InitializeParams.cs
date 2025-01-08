using AIs;

using HarmonyLib;

using System;
using System.Collections.Generic;

namespace GHTweaks.Patches
{
	[HarmonyPatchCategory(PatchCategory.AIManager)]
	[HarmonyPatch(typeof(AIManager), "InitializeAIParams")]
	internal class AIManagerInitializeParams
    {
		static void Postfix(AIManager __instance) 
		{
			var csv = new List<string>
            {
                GetCsvHeader()
            };

			foreach(var kvp in __instance.m_AIParamsMap)
				csv.Add(ConvertAIParamsToCSVLine(kvp.Key, kvp.Value));

			System.IO.File.WriteAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "AIParams.csv"), string.Join(Environment.NewLine, csv));
		}

		static string GetCsvHeader()
		{
			var observedProperties = new string[]
			{
				"AI_ID",
				"Health",
				"HealthRegeneration",
				"AttackRange",
				"Damage",
				"JumpAttackRange",
				"JumpBackRange",
				"PoisonLevel",
				"MinBitingDuration",
				"MaxBitingDuration",
				"EnemySenseRange",
				"SightAngle",
				"SightRange",
				"HearingSneakRange",
				"HearingWalkRange",
				"HearingRunRange",
				"HearingSwimRange",
				"HearingActionRange"
			};
			return $"\"{string.Join("\";\"", observedProperties)}\"";
		}

		static string ConvertAIParamsToCSVLine(int key, AIParams aiParams)
		{
			var observedProperties = new string[]
			{
				((AI.AIID)key).ToString(),
				aiParams.m_Health.ToString(),
				aiParams.m_HealthRegeneration.ToString(),
				aiParams.m_AttackRange.ToString(),
				aiParams.m_Damage.ToString(),
				aiParams.m_JumpAttackRange.ToString(),
				aiParams.m_JumpBackRange.ToString(),
				aiParams.m_PoisonLevel.ToString(),
				aiParams.m_MinBitingDuration.ToString(),
				aiParams.m_MaxBitingDuration.ToString(),
				aiParams.m_EnemySenseRange.ToString(),
				aiParams.m_SightAngle.ToString(),
				aiParams.m_SightRange.ToString(),
				aiParams.m_HearingSneakRange.ToString(),
				aiParams.m_HearingWalkRange.ToString(),
				aiParams.m_HearingRunRange.ToString(),
				aiParams.m_HearingSwimRange.ToString(),
				aiParams.m_HearingActionRange.ToString()
			};
			return $"\"{string.Join("\";\"", observedProperties)}\"";
		}
	}
}
