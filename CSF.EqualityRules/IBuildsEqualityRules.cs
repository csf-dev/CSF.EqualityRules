using System.Collections.Generic;
using CSF.EqualityRules.Builders;

namespace CSF.EqualityRules
{
    public interface IBuildsEqualityRules<T>
    {
        ISet<RuleBuilder<T>> RuleBuilders { get; }
    }
}