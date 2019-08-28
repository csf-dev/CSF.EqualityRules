using System;
using System.Collections.Generic;

namespace CSF.EqualityRules
{
    public interface IEqualityRule<in T> : IEqualityComparer<T>
    {
        string Name { get; }
        EqualityResult GetEqualityResult(T x, T y);
    }
}