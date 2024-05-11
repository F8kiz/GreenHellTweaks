using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Player)]
    [HarmonyPatch(typeof(Player), ".ctor", MethodType.Constructor)]
    internal class PlayerConstructor
    {
        static void Postfix(Player __instance)
        {
            if (Mod.Instance.Config.PlayerConfig.MinFallingHeightToDealDamage > 0)
            {
                AccessTools.FieldRef<Player, float> minFallingHeightToDealDamage = AccessTools.FieldRefAccess<Player, float>("m_MinFallingHeightToDealDamage");
                minFallingHeightToDealDamage(__instance) = Mod.Instance.Config.PlayerConfig.MinFallingHeightToDealDamage;
            }
        }
    }
}