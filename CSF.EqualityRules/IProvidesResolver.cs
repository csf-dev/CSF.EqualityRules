namespace CSF.EqualityRules
{
    /// <summary>
    /// An object which can provide a service resolver, for integrating with dependency injection.
    /// </summary>
    public interface IProvidesResolver
    {
        /// <summary>
        /// Gets the service resolver.
        /// </summary>
        /// <returns>The resolver.</returns>
        IResolvesServices GetResolver();
    }
}