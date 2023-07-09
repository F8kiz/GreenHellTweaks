using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatch(typeof(FoodInfo), "m_SpoilTime", MethodType.Setter)]
    internal class FoodInfoSpoilTime
    {
        static void Prefix(FoodInfo __instance, ref float value)
        {
            if (value < 1)
                return;
            
            //Mod.Instance.WriteLog($"set m_SpoilTime => m_BaseItemID: {__instance.m_BaseItemID,-30:C}, m_MeatType: {__instance.m_MeatType,-30:C}, m_CookingItemID: {__instance.m_CookingItemID,-30:C} = {value}");
            //Mod.Instance.WriteLog($"set m_SpoilTime => isMeat: {__instance.IsMeat().ToString().PadRight(6)} m_BaseItemID: {__instance.m_BaseItemID.ToString().PadRight(25)} m_MeatType: {__instance.m_MeatType.ToString().PadRight(25)} m_CookingItemID: {__instance.m_CookingItemID.ToString().PadRight(21)} = {value}");

            if (__instance.IsMeat() && Mod.Instance.Config.FoodInfoConfig.SpoilTime > 0)
            {
                value = Mod.Instance.Config.FoodInfoConfig.SpoilTime;
                Mod.Instance.WriteLog($"set MeatSpoilTime: {value}");
            }
        }
    }
}
