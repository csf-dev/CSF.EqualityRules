using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSF.EqualityRules.Internal;

namespace CSF.EqualityRules.Builders
{
    public abstract class RuleBuilder<T> : IBuildsRule
    {
        public virtual string Name { get; set; }

        public abstract IEnumerable<IEqualityRule<T>> GetRules(IEnumerable<RuleBuilder<T>> allBuilders);

        IEnumerable<IEqualityRule> IBuildsRule.GetRules(IEnumerable<IBuildsRule> allBuilders)
        {
            var otherBuilders = allBuilders.OfType<RuleBuilder<T>>().ToArray();
            return GetRules(otherBuilders);
        }

        internal static Func<IEqualityComparer<TCompared>> GetDefaultComparerFactory<TCompared>()
            => () => EqualityComparer<TCompared>.Default;

        internal static Func<IEqualityComparer> GetDefaultComparerFactory()
            => () => new DefaultNonGenericComparerAdapter();
    }
}
