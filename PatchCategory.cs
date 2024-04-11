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
    }
}
