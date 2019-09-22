using System;

namespace CSF.EqualityRules
{
    /// <summary>
    /// A resolver object, which can resolve other services as-required.  Use for integrating with DI frameworks.
    /// </summary>
    public interface IResolvesServices
    {
        /// <summary>
        /// Gets/resolves an object of a specified type.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <typeparam name="T">The type of object to resolve.</typeparam>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Gets/resolves an object of a specified type.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <param name="type">The type of object to resolve.</param>
        object Resolve(Type type);
    }
}