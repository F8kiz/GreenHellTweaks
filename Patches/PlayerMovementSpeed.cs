using GHTweaks.Configuration;
using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.PlayerMovement)]
    [HarmonyPatch(typeof(FPPController), "UpdateWantedSpeed")]
    internal class FPPControllerUpdateWantedSpeed
    {
        static void Prefix(FPPController __instance)
        {
            PlayerMovementConfig config = Mod.Instance.Config.PlayerMovementConfig;
            if (config.WalkSpeed > 0)
                __instance.m_WalkSpeed = config.WalkSpeed;

            if (config.BackwardWalkSpeed > 0)
                __instance.m_BackwardWalkSpeed = config.BackwardWalkSpeed;

            if (config.RunSpeed > 0)
                __instance.m_RunSpeed = config.RunSpeed;

            if (config.DuckSpeedMultiplier > 0)
                __instance.m_DuckSpeedMul = config.DuckSpeedMultiplier;

            if (config.MaxOverloadSpeedMultiplier > 0)
                __instance.m_MaxOverloadSpeedMul = config.MaxOverloadSpeedMultiplier;

        }
    }

    [HarmonyPatchCategory(PatchCategory.PlayerMovement)]
    [HarmonyPatch(typeof(SwimController), ".ctor", MethodType.Constructor)]
    internal class SwimControllerSpeedAddMax
    {
        static void Postfix(SwimController __instance)
        {
            PlayerMovementConfig config = Mod.Instance.Config.PlayerMovementConfig;
            if (config.MaxSwimSpeed > 0)
            {
                AccessTools.FieldRef<SwimController, float> mSpeedAddMax = AccessTools.FieldRefAccess<SwimController, float>("m_SpeedAddMax");
                mSpeedAddMax(__instance) = config.MaxSwimSpeed;
            }
        }
    }
}
