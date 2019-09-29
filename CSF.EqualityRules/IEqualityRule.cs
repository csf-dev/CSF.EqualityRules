using System;
using System.Collections.Generic;

namespace CSF.EqualityRules
{
    /// <summary>
    /// Non-generic interface which described an equality rule.
    /// </summary>
    public interface IEqualityRule
    {
        /// <summary>
        /// Gets the rule name.
        /// </summary>
        /// <value>The rule name.</value>
        string Name { get; }
    }

    /// <summary>
    /// Describes an equality rule, generic for a type which is being tested.
    /// </summary>
    public interface IEqualityRule<in T> : IEqualityComparer<T>, IGetsEqualityResult<T>, IEqualityRule
    {
    }
}
