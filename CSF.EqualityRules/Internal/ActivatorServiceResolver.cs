using System;

namespace CSF.EqualityRules.Internal
{
    /// <summary>
    /// Default implementation of <see cref="IResolvesServices"/> which uses <c>Activator.CreateInstance</c> to
    /// resolve service types.
    /// </summary>
    public class ActivatorServiceResolver : IResolvesServices
    {
        /// <summary>
        /// Gets/resolves an object of a specified type.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <typeparam name="T">The type of object to resolve.</typeparam>
        public T Resolve<T>() where T : class => Activator.CreateInstance<T>();

        /// <summary>
        /// Gets/resolves an object of a specified type.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <param name="type">The type of object to resolve.</param>
        public object Resolve(Type type) => Activator.CreateInstance(type);
    }
}