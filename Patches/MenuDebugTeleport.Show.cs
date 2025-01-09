using HarmonyLib;

using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.GreenHellGameUpdate)]
    [HarmonyPatch(typeof(MenuDebugTeleport), "OnShow")]
    internal class MenuDebugTeleportShow
    {
        static void Postfix(MenuDebugTeleport __instance)
        {
            Vector3 worldPosition = Player.Get().GetWorldPosition();
            __instance.m_PosX.text = worldPosition.x.ToString();
            __instance.m_PosY.text = worldPosition.y.ToString();
            __instance.m_PosZ.text = worldPosition.z.ToString();
        }
    }
}
