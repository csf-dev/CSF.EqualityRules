using System;
using System.Collections.Generic;

namespace CSF.EqualityRules
{
    public interface IEqualityRule
    {
        string Name { get; }
    }

    public interface IEqualityRule<in T> : IEqualityComparer<T>, IGetsEqualityResult<T>, IEqualityRule
    {
    }
}