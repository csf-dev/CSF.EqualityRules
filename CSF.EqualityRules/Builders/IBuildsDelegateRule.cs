using System;

namespace CSF.EqualityRules.Builders
{
    public interface IBuildsDelegateRule<TParent> : IBuildsRule
    {
        Func<TParent, object> Delegate { get; }
    }
}
