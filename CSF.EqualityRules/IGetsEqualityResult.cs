using System;
using System.Collections.Generic;

namespace CSF.EqualityRules
{
    /// <summary>
    /// A specialisation of <see cref="IEqualityComparer{T}"/> which provides a detailled result object with
    /// information.
    /// </summary>
    public interface IGetsEqualityResult<in T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Gets a detailled equality comparison result.
        /// </summary>
        /// <returns>The equality result.</returns>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        EqualityResult GetEqualityResult(T x, T y);
    }
}
