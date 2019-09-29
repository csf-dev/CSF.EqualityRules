using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSF.EqualityRules.Internal;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// Base type for a service which builds a collection of equality rules.
    /// </summary>
    public abstract class RuleBuilder<T> : IBuildsRule
    {
        /// <summary>
        /// Gets or sets the name for this rule-builder.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Builds and returns a collection of <see cref="IEqualityRule{T}"/>.
        /// </summary>
        /// <returns>The rules.</returns>
        /// <param name="allBuilders">A collection of all rule-builders.</param>
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
