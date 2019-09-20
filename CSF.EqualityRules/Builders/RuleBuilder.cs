using System;
using System.Collections;
using System.Collections.Generic;
using CSF.EqualityRules.Internal;

namespace CSF.EqualityRules.Builders
{
    public abstract class RuleBuilder<T> : IBuildsRule
    {
        public virtual string Name { get; set; }
        public abstract IEnumerable<IEqualityRule<T>> GetRules();

        internal static Func<IEqualityComparer<TCompared>> GetDefaultComparerFactory<TCompared>()
            => () => EqualityComparer<TCompared>.Default;

        internal static Func<IEqualityComparer> GetDefaultComparerFactory()
            => () => new DefaultNonGenericComparerAdapter();
    }
}
