using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Torch)]
    [HarmonyPatch(typeof(Torch), nameof(Torch.StartBurning))]
    internal class TorchStartBurning
    {
        static void Postfix(Torch __instance)
        {
            __instance.m_DebugInfiniteBurn = Mod.Instance.Config.TorchConfig.InfiniteBurn;
        }
    }
}
