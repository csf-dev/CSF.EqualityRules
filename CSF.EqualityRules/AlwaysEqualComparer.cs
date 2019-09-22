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
        public new bool Equals(object x, object y) => true;

        public int GetHashCode(object obj) => 0;
    }

    /// <summary>
    /// Implementation of <see cref="IEqualityComparer{T}"/> which always considers objects to be equal and assigns
    /// a hash code of zero to everything.
    /// </summary>
    public class AlwaysEqualComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y) => true;

        public int GetHashCode(T obj) => 0;
    }
}
