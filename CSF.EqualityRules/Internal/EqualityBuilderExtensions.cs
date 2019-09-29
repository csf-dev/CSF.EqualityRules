using System;
using CSF.EqualityRules.Builders;

namespace CSF.EqualityRules.Internal
{
    /// <summary>
    /// Extension methods which expose framework/internal equality-builder functionality.
    /// </summary>
    public static class EqualityBuilderExtensions
    {
        /// <summary>
        /// Gets the equality builder as a <see cref="IProvidesRuleBuilders{T}"/>.
        /// </summary>
        /// <returns>The rule builder provider.</returns>
        /// <param name="builder">Equality builder.</param>
        /// <typeparam name="T">The type for which the equality builder was created.</typeparam>
        public static IProvidesRuleBuilders<T> AsRuleBuilderProvider<T>(this EqualityBuilder<T> builder) => builder;

        /// <summary>
        /// Gets the equality builder as a <see cref="IResolvesServices"/>.
        /// </summary>
        /// <returns>The service resolver.</returns>
        /// <param name="builder">Equality builder.</param>
        /// <typeparam name="T">The type for which the equality builder was created.</typeparam>
        public static IResolvesServices GetServiceResolver<T>(this EqualityBuilder<T> builder)
        {
            return ((IProvidesResolver) builder).GetResolver();
        }

        /// <summary>
        /// Gets an instance of <see cref="ComparerFactoryBuilder{T}"/> for the equality builder.
        /// </summary>
        /// <returns>The comparer factory builder.</returns>
        /// <param name="builder">Equality builder.</param>
        /// <typeparam name="TEquality">The type for which the equality builder was created.</typeparam>
        /// <typeparam name="TCompared">The value type to be compared.</typeparam>
        public static ComparerFactoryBuilder<TCompared> GetComparerFactoryBuilder<TEquality,TCompared>(this EqualityBuilder<TEquality> builder)
        {
            var resolver = builder.GetServiceResolver();
            return new ComparerFactoryBuilder<TCompared>(resolver);
        }
    }
}
