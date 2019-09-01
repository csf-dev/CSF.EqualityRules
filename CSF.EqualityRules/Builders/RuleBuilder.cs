using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Builders
{
    public abstract class RuleBuilder<T> : IBuildsRule
    {
        public virtual string Name { get; set; }
        public abstract IEnumerable<IEqualityRule<T>> GetRules();
    }
}
