using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// An object which chooses how a value should be compared.
    /// </summary>
    public interface IBuildsComparerFactory<out TEquality>
    {
        /// <summary>
        /// Indicates that a specific comparer type should be used for testing equality.
        /// </summary>
        /// <typeparam name="TComparer">The comparer type to use.</typeparam>
        void UsingComparer<TComparer>() where TComparer : class,IEqualityComparer<TEquality>;

        /// <summary>
        /// Indicates that a specific comparer type should be used for testing equality.
        /// </summary>
        /// <param name="comparerType">The comparer type to use.</param>
        void UsingComparer(Type comparerType);

        /// <summary>
        /// Indicates that a specific comparer instance should be used for testing equality.
        /// </summary>
        /// <param name="comparer">The comparer to use.</param>
        void UsingComparer(IEqualityComparer<TEquality> comparer);

        /// <summary>
        /// Indicates that no comparison should be performed (use a comparer which always returns <c>true</c>).
        /// </summary>
        void Ignore();
    }
}