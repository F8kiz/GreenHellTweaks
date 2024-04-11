using HarmonyLib;

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
        static bool Prefix()
        {
            if (Mod.Instance.Config.PlayerConfig.SteadyAim)
            {
                HUDCrosshair.Get().SetAimPower(1f);
                PlayerConditionModule.Get().m_Stamina = 100f;
                return false;
            }
            return true;
        }
    }
}
