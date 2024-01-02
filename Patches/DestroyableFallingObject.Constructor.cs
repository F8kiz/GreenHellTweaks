using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(DestroyableFallingObject), ".ctor", MethodType.Constructor)]
    internal class DestroyableFallingObjectConstructor
    {
        static void Postfix(DestroyableFallingObject __instance)
        {
            if (Mod.Instance.Config.DestroyableFallingObjectConfig.MaxTimeToDestroy > 0)
            {
                AccessTools.FieldRef<DestroyableFallingObject, float> minFallingHeightToDealDamage = AccessTools.FieldRefAccess<DestroyableFallingObject, float>("m_MaxTimeToDestroy");
                minFallingHeightToDealDamage(__instance) = Mod.Instance.Config.DestroyableFallingObjectConfig.MaxTimeToDestroy;
            }
		}
    }
}
