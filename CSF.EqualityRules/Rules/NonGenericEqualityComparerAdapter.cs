using System;
using System.Collections;
using System.Collections.Generic;

namespace CSF.EqualityRules.Rules
{
    /// <summary>
    /// An adapter type which allows a non-generic <see cref="IEqualityComparer"/> to be used as
    /// an <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    public class NonGenericEqualityComparerAdapter<T> : EqualityComparer<T>
    {
        readonly IEqualityComparer comparer;

        /// <summary>When overridden in a derived class, determines whether two objects of type <typeparamref name="T"/> name="T" /> are equal.</summary>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public override bool Equals(T x, T y) => comparer.Equals(x, y);

        /// <summary>When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.</summary>
        /// <returns>A hash code for the specified object.</returns>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
        public override int GetHashCode(T obj) => comparer.GetHashCode(obj);

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NonGenericEqualityComparerAdapter{T}"/> class.
        /// </summary>
        /// <param name="comparer">The wrapped comparer.</param>
        public NonGenericEqualityComparerAdapter(IEqualityComparer comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }
    }
}