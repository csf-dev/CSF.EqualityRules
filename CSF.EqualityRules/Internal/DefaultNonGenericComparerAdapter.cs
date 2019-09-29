using System;
using System.Collections;
using System.Collections.Generic;

namespace CSF.EqualityRules.Internal
{
    /// <summary>
    /// An adapter type which allows the default equality comparer for <see cref="Object"/> to be used as a
    /// non-generic <see cref="IEqualityComparer"/>.
    /// </summary>
    public class DefaultNonGenericComparerAdapter : IEqualityComparer, IEqualityComparer<object>
    {
        static readonly IEqualityComparer<object> wrapped;

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <exception cref="T:System.ArgumentException" />
        /// <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other.
        public new bool Equals(object x, object y)
        {
            return wrapped.Equals(x, y);
        }

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <returns>A hash code for the specified object.</returns>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
        public int GetHashCode(object obj)
        {
            return wrapped.GetHashCode(obj);
        }

        static DefaultNonGenericComparerAdapter()
        {
            wrapped = EqualityComparer<object>.Default;
        }
    }
}
