﻿using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.ItemInfo)]
    [HarmonyPatch(typeof(ItemInfo), nameof(ItemInfo.m_Health), MethodType.Setter)]
    internal class ItemInfoConfigHealth
    {
        static void Prefix(ItemInfo __instance, ref float value)
        {
            if (Mod.Instance.Config.ItemInfoConfig.UnbreakableWeapons && (__instance.IsAxe() || __instance.IsBlowpipe() || __instance.IsBow() || __instance.IsKnife() ||
                __instance.IsMachete() || __instance.IsSpear() || __instance.IsWeapon()) && value < __instance.m_MaxHealth)
                value = __instance.m_MaxHealth;

            if (Mod.Instance.Config.ItemInfoConfig.UnbreakableArmor && __instance.IsArmor() && value < __instance.m_MaxHealth)
                value = __instance.m_MaxHealth;
        }
    }
}
