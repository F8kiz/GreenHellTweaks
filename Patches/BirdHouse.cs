using HarmonyLib;

namespace GHTweaks.Patches
{
    [HarmonyPatchCategory(PatchCategory.BirdHouse)]
    [HarmonyPatch(typeof(BirdHouse), ".ctor", MethodType.Constructor)]
    internal class BirdHouseConstructor
    {
        static void Postfix(BirdHouse __instance)
        {
#if DEBUG
            LogWriter.Write($"BirdHouse.Constructor.Postfix enter BridHouse constructor");
#endif 
            var config = Mod.Instance.Config.BirdHouseConfig;

            // The default value is 2, so we just need to patch if the value is 1 or greater than 2.
            if (config.MaxBirdsCount == 1 || config.MaxBirdsCount > 2)
            {
#if DEBUG
                LogWriter.Write($"BirdHouse.Constructor.Postfix set m_MaxBirdsCount: {config.MaxBirdsCount}");
#endif 
                AccessTools.FieldRef<BirdHouse, int> maxBirdsCount = AccessTools.FieldRefAccess<BirdHouse, int>("m_MaxBirdsCount");
                maxBirdsCount(__instance) = config.MaxBirdsCount;
            }

            if (config.SpawnBirdsDelay > 0 && config.SpawnBirdsDelay != 2)
            {
#if DEBUG
                LogWriter.Write($"BirdHouse.Constructor.Postfix set m_SpawnBirdsDelay: {config.SpawnBirdsDelay}");
#endif 
                AccessTools.FieldRef<BirdHouse, float> spawnBirdsDelay = AccessTools.FieldRefAccess<BirdHouse, float>("m_SpawnBirdsDelay");
                spawnBirdsDelay(__instance) = config.SpawnBirdsDelay;
            }

            if (config.DomesticationTime > 0 && config.DomesticationTime != 6)
            {
#if DEBUG
                LogWriter.Write($"BirdHouse.Constructor.Postfix set m_DomesticationTime: {config.DomesticationTime}");
#endif 
                AccessTools.FieldRef<BirdHouse, float> domesticationTime = AccessTools.FieldRefAccess<BirdHouse, float>("m_DomesticationTime");
                domesticationTime(__instance) = config.DomesticationTime;
            }

            if (config.BirdKillPenaltyTime > 0 && config.BirdKillPenaltyTime != 6)
            {
#if DEBUG
                LogWriter.Write($"BirdHouse.Constructor.Postfix set m_BirdKillPenaltyTime: {config.BirdKillPenaltyTime}");
#endif 
                AccessTools.FieldRef<BirdHouse, float> birdKillPenaltyTime = AccessTools.FieldRefAccess<BirdHouse, float>("m_BirdKillPenaltyTime");
                birdKillPenaltyTime(__instance) = config.BirdKillPenaltyTime;
            }
        }
    }

    [HarmonyPatchCategory(PatchCategory.BirdHouse)]
    [HarmonyPatch(typeof(BirdHouse), "UpdateDomestication")]
    internal class BirdHouseUpdateDomentication
    {
        static void Postfix(BirdHouse __instance)
        {
            AccessTools.FieldRef<BirdHouse, LandingSpotController> landingSpotController = AccessTools.FieldRefAccess<BirdHouse, LandingSpotController>("m_LandingSpotController");
            var distance = landingSpotController(__instance)._maxBirdDistance * 10;
            landingSpotController(__instance)._maxBirdDistance = distance;
        }
    }
}
