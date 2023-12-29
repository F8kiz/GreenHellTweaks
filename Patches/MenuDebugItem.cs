﻿using Enums;

using GHTweaks.Utilities;

using HarmonyLib;

using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
using UnityEngine.EventSystems;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(MenuDebugItem), "OnShow")]
    internal class MenuDebugItemOnShow
    {
        static void Postfix(MenuDebugItem __instance)
        {
            __instance.m_List.SetFocus(true);
            __instance.m_Field.text = string.Empty;
            EventSystem.current.SetSelectedGameObject(__instance.m_Field.gameObject, null);
            __instance.m_Field.OnPointerClick(new PointerEventData(EventSystem.current));

            //this.m_LastField = string.Empty;
            AccessTools.FieldRef<MenuDebugItem, string> lastField = AccessTools.FieldRefAccess<MenuDebugItem, string>("m_LastField");
            lastField(__instance) = string.Empty;

            //this.Setup();
            MethodInfo miSetup = typeof(MenuDebugItem).GetMethod("Setup", BindingFlags.Instance | BindingFlags.NonPublic);
            miSetup.Invoke(__instance, null);
        }
    }

    [HarmonyPatch(typeof(MenuDebugItem), "OnHide")]
    internal class MenuDebugOnHide
    {
        static void Prefix(MenuDebugItem __instance)
        {
            string selectedElementText = __instance.m_List.GetSelectedElementText();
            if (!string.IsNullOrEmpty(selectedElementText))
            {
                if (!ItemSpawner.TrySpawnItem(selectedElementText))
                {
                    Mod.Instance.PrintMessage($"The item ({selectedElementText}) could not be spawned.", LogType.Error);
                }
            }
        }
    }

    [HarmonyPatch(typeof(MenuDebugItem), "Setup")]
    internal class MenuDebugSetup
    {
        static void Postfix(MenuDebugItem __instance)
        {
            if (!__instance.m_List)
            {
                return;
            }

            FieldInfo fiItems = AccessTools.Field(typeof(MenuDebugItem), "m_Items");
            Dictionary<int, ItemInfo> items = (Dictionary<int, ItemInfo>)fiItems.GetValue(__instance);

            FieldInfo fiLastField = AccessTools.Field(typeof(MenuDebugItem), "m_LastField");
            string lastField = (string)fiLastField.GetValue(__instance);

            __instance.m_List.Clear();
            items = ItemsManager.Get().GetAllInfos();
            using (Dictionary<int, ItemInfo>.KeyCollection.Enumerator enumerator = items.Keys.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ItemID itemID = (ItemID)enumerator.Current;
                    string text = string.Empty;
                    text += itemID.ToString();
                    if (text.ToLower().Contains(lastField))
                    {
                        __instance.m_List.AddElement(text, -1);
                    }
                }
            }
            __instance.m_List.SortAlphabetically();
            __instance.m_List.SetSelectionIndex(0);
        }
    }

    [HarmonyPatch(typeof(MenuDebugItem), "Update")]
    internal class MenuDebugUpdate
    {
        static void Postfix(MenuDebugItem __instance)
        {
            AccessTools.FieldRef<MenuDebugItem, string> lastField = AccessTools.FieldRefAccess<MenuDebugItem, string>("m_LastField");

            if (lastField(__instance) != __instance.m_Field.text)
            {
                lastField(__instance) = __instance.m_Field.text.ToLower();

                MethodInfo miSetup = typeof(MenuDebugItem).GetMethod("Setup", BindingFlags.Instance | BindingFlags.NonPublic);
                miSetup.Invoke(__instance, null);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                __instance.OnClose();
            }
        }
    }
}