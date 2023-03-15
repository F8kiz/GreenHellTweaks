using AIs;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(DeadBody), nameof(DeadBody.GetActions))]
    internal class DeadBodyGetAction
    {
        static List<TriggerAction.TYPE> lstActions;

        static void Prefix(List<TriggerAction.TYPE> actions) => lstActions = actions;
        

        static void Postfix(ref List<TriggerAction.TYPE> actions)
        {
            if (lstActions.Count == actions.Count && HasPlayerSomeKindOfBlade())
                actions.Add(TriggerAction.TYPE.HarvestHold);
        }

        static bool HasPlayerSomeKindOfBlade()
        {
            Player p = Player.Get();
            Item currentItem = p.GetCurrentItem(Enums.Hand.Right);
            if (currentItem && currentItem.m_Info.IsMachete())
                return true;

            return InventoryBackpack.Get().m_Items.FirstOrDefault(x => x.m_Info.IsMachete()) != null;
        }
    }
}
