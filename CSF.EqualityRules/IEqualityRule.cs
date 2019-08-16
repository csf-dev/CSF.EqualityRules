using System;
using System.Collections.Generic;

namespace CSF.EqualityRules
{
    public interface IEqualityRule<in T> : IEqualityComparer<T>
    {
        string Name { get; }
        event EventHandler<RuleCompletedEventArgs> RuleCompleted;
        event EventHandler<RuleErroredEventArgs> RuleErrored;
    }
}