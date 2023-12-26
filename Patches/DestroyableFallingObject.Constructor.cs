using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(DestroyableFallingObject), ".ctor", MethodType.Constructor)]
    internal class DestroyableFallingObjectConstructor
    {
        static void Postfix(DestroyableFallingObject __instance)
        {
			AccessTools.FieldRef<DestroyableFallingObject, float> minFallingHeightToDealDamage = AccessTools.FieldRefAccess<DestroyableFallingObject, float>("m_MaxTimeToDestroy");
			minFallingHeightToDealDamage(__instance) = float.MaxValue;
		}
    }
}
