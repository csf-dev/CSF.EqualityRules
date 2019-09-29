using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// Default implementation of <see cref="IBuildsComparerFactory{TEquality}"/>, allowing the selection of
    /// an equality comparer implementation.
    /// </summary>
    public class ComparerFactoryBuilder<T> : IBuildsComparerFactory<T>
    {
        readonly IResolvesServices resolver;

        /// <summary>
        /// Gets the comparer.
        /// </summary>
        /// <value>The comparer.</value>
        public Func<IEqualityComparer<T>> Comparer { get; private set; }

        /// <summary>
        /// Indicates that a specific comparer type should be used for testing equality.
        /// </summary>
        /// <typeparam name="TComparer">The comparer type to use.</typeparam>
        public void UsingComparer<TComparer>() where TComparer : class,IEqualityComparer<T>
        {
            Comparer = () => resolver.Resolve<TComparer>();
        }

        /// <summary>
        /// Indicates that a specific comparer type should be used for testing equality.
        /// </summary>
        /// <param name="comparerType">The comparer type to use.</param>
        public void UsingComparer(Type comparerType)
        {
            Comparer = () => (IEqualityComparer<T>) resolver.Resolve(comparerType);
        }

        /// <summary>
        /// Indicates that a specific comparer instance should be used for testing equality.
        /// </summary>
        /// <param name="comparer">The comparer to use.</param>
        public void UsingComparer(IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            Comparer = () => comparer;
        }

        /// <summary>
        /// Indicates that no comparison should be performed (use a comparer which always returns <c>true</c>).
        /// </summary>
        public void Ignore()
        {
            Comparer = () => new AlwaysEqualComparer<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparerFactoryBuilder{T}"/> class.
        /// </summary>
        /// <param name="resolver">A service resolver.</param>
        public ComparerFactoryBuilder(IResolvesServices resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            Comparer = null;
        }
    }
}