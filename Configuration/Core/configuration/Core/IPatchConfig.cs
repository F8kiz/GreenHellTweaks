namespace GHTweaks.Configuration.Core
{
    public interface IPatchConfig
    {
        /// <summary>
        /// Returns true if at least one property enabled.
        /// If the returned value equals to true the patch category should be patched.
        /// </summary>
        bool HasAtLeastOneEnabledPatch { get; }
    }
}
