using System.Security.Permissions;

namespace GHTweaks
{
    internal static class PatchCategory
    {
        /// <summary>
        /// Required patches are always applied.
        /// </summary>
        public const string Required = "Required";

        /// <summary>
        /// Default patches are always applied when the user plays offline.
        /// </summary>
        public const string Default = "Default";

        /// <summary>
        /// Cheat patches are applied if the user has activated them in the configuration.
        /// </summary>
        public const string Cheats = "Cheats";

        public const string MenuDebug = "MenuDebug";

        public const string AISoundModule = "AISoundModule";

        public const string Construction = "Construction";

        public const string DestroyFallingObjects = "DestroyFallingObjects";

        public const string FireCamp = "FireCamp";

        public const string FoodInfo = "FoodInfo";

        public const string InventoryBackpack = "InventroyBackpack";

        public const string ItemInfo = "ItemInfo";

        public const string LiquidContainer = "LiquidContainer";

        public const string PlayerConditionModul = "PlayerConditionModul";

        public const string Player = "Player";

        public const string PlayerMovement = "PlayerMovement";

        public const string PocketGrid = "PocketGrid";

        public const string Skill = "Skill";

        public const string TimeOfDay = "TimeOfDay";

        public const string Torch = "Torch";
    }
}
