using GHTweaks.Configuration;

using HarmonyLib;

namespace GHTweaks.Patches
{
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
