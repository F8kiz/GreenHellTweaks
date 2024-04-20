using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.Construction)]
    [HarmonyPatch(typeof(Construction), nameof(Construction.DestroyMe))]
    internal class ConstructionDestroyMe
    {
        static void Prefix(out bool create_items)
        {
            create_items = !Mod.Instance.Config.ConstructionConfig.DestroyConstructionWithoutItems;
        }
    }
}
