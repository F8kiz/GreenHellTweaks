using HarmonyLib;
using System.Reflection;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(Torch), "CheckIfInBackPack")]
    internal class TorchCheckIfInBackPack
    {
        static void Prefix(Torch __instance)
        {
            FieldInfo fiWasInBackpack = AccessTools.Field(typeof(Torch), "m_WasInBackPackLastFrame");
            bool wasInBackpack = (bool)fiWasInBackpack.GetValue(__instance);

            FieldInfo fiIsBurning = AccessTools.Field(typeof(Torch), "m_Burning");
            bool isBurning = (bool)fiIsBurning.GetValue(__instance);

            if (!InventoryBackpack.Get().Contains(__instance))
            {
                if (InventoryBackpack.Get().Contains(__instance) != wasInBackpack)
                {
                    Mod.Instance.WriteLog("Backpack does not contains instance");
                    if (!isBurning)
                        __instance.StartBurning();
                    return;
                }
            }
            else
            {
                Mod.Instance.WriteLog("Backpack contains instance");
                if (isBurning)
                    __instance.Extinguish();
            }
        }
    }
}
