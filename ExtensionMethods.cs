namespace GHTweaks
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts the given boolean into an known string.
        /// </summary>
        /// <param name="b">The boolean which should be converted.</param>
        /// <returns>Enabled if the bool has the value TRUE, otherwise disabled.</returns>
        public static string ToKnownState(this bool b) => b ? "enabled" : "disabled";
    }
}
