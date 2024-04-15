using HarmonyLib;

using System.Reflection;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(Player), nameof(Player.StartShake))]
    internal class PlayerStartShake
    {
        static void Postfix(Player __instance)
        {
            if (Mod.Instance.Config.PlayerConfig.SteadyAim)
            {
                AccessTools.FieldRef<Player, float> wantedShakePower = AccessTools.FieldRefAccess<Player, float>("m_WantedShakePower");
                AccessTools.FieldRef<Player, float> shakeSpeed = AccessTools.FieldRefAccess<Player, float>("m_ShakeSpeed");
                AccessTools.FieldRef<Player, float> shakePowerDuration = AccessTools.FieldRefAccess<Player, float>("m_SetShakePowerDuration");

                wantedShakePower(__instance) = 0f;
                shakeSpeed(__instance) = 0f;
                shakePowerDuration(__instance) = 0f;
            }
        }
    }

    [HarmonyPatchCategory(PatchCategory.Default)]
    [HarmonyPatch(typeof(Player), "UpdateAim")]
    internal class PlayerAimPower
    {
        static bool Prefix(Player __instance)
        {
            if (Mod.Instance.Config.PlayerConfig.SteadyAim)
            {
                FieldInfo fiAim = AccessTools.Field(typeof(Player), "m_Aim");
                if (fiAim == null)
                {
                    Mod.Instance.WriteLog($"PlayerAimPower: Failed to access m_Aim field!", LogType.Error);
                    return false;
                }

                bool aim = (bool)fiAim.GetValue(__instance);
                if (aim)
                {
                    HUDCrosshair.Get().SetAimPower(1f);
                    PlayerConditionModule.Get().m_Stamina = 100f;
                    return false;
                }
            }
            return true;
        }
    }
}
