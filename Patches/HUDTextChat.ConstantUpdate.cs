﻿using HarmonyLib;
using UnityEngine;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(HUDTextChat), "ConstantUpdate")]
    internal class HUDTextChatConstantUpdate
    {
        static bool showTextChat = false;


        static void Prefix(HUDTextChat __instance)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                showTextChat = !showTextChat;
                AccessTools.FieldRef<HUDTextChat, bool> hudChatShouldBeVisible = AccessTools.FieldRefAccess<HUDTextChat, bool>("m_ShouldBeVisible");
                hudChatShouldBeVisible(__instance) = showTextChat;
                Mod.Instance.PrintMessage($"shouldBeVisible: {hudChatShouldBeVisible(__instance)}, showTextChat: {showTextChat}");
            }
        }
    }
}