using HarmonyLib;

namespace GHTweaks.Patches
{
	[HarmonyPatch(typeof(Construction), "SetUpperLevel")]
	internal class ConstructionSetUpperLevel
	{
		static void Prefix(ref int level)
		{
            if (Mod.Instance.Config.ConstructionConfig.PlaceEveryWhereEnabled && level > 1)
                level = 1;
        }
    }
}
