using System;
using System.Collections;
using System.Collections.Generic;

namespace CSF.EqualityRules
{
    /// <summary>
    /// Implementation of <see cref="IEqualityComparer"/> which always considers objects to be equal and assigns
    /// a hash code of zero to everything.
    /// </summary>
    public class AlwaysEqualComparer : IEqualityComparer
    {
        /// <summary>
        /// Always returns <c>true</c>, indicating the objects are equal.
        /// </summary>
        /// <returns>Hard-coded true.</returns>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        public new bool Equals(object x, object y) => true;

        /// <summary>
        /// Always returns zero, a constant hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        /// <param name="obj">Object.</param>
        public int GetHashCode(object obj) => 0;
    }

    /// <summary>
    /// Implementation of <see cref="IEqualityComparer{T}"/> which always considers objects to be equal and assigns
    /// a hash code of zero to everything.
    /// </summary>
    public class AlwaysEqualComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Always returns <c>true</c>, indicating the objects are equal.
        /// </summary>
        /// <returns>Hard-coded true.</returns>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        public bool Equals(T x, T y) => true;

        /// <summary>
        /// Always returns zero, a constant hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        /// <param name="obj">Object.</param>
        public int GetHashCode(T obj) => 0;
    }
}
