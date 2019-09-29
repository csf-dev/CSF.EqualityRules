using System;
using System.Collections.Generic;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// Delegate rule builder.
    /// </summary>
    public class DelegateRuleBuilder<TParent,TValue> : RuleBuilder<TParent>, IBuildsDelegateRule<TParent>
    {
        /// <summary>
        /// Gets the delegate used to retrieve the value for comparison.
        /// </summary>
        /// <value>The delegate.</value>
        public Func<TParent, TValue> Delegate { get; }

        /// <summary>
        /// Gets a function which will create a comparer for the generated rules.
        /// </summary>
        /// <value>The comparer.</value>
        public Func<IEqualityComparer<TValue>> Comparer { get; }

        Func<TParent, object> IBuildsDelegateRule<TParent>.Delegate => p => Delegate(p);

        /// <summary>
        /// Builds and returns a collection of <see cref="IEqualityRule{T}"/>.
        /// </summary>
        /// <returns>The rules.</returns>
        /// <param name="allBuilders">A collection of all rule-builders.</param>
        public override IEnumerable<IEqualityRule<TParent>> GetRules(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var delegateRule = new EqualityRule<TValue>(Comparer(), Name);
            var valueProvider = new DelegateValueProvider<TParent, TValue>(Delegate);
            var parentRule = new ParentEqualityRule<TParent, TValue>(valueProvider, delegateRule);
            return new[] { parentRule };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateRuleBuilder{TParent, TValue}"/> class.
        /// </summary>
        /// <param name="valueDelegate">The delegate which will be used to retrieve a value for comparison.</param>
        /// <param name="name">The rule name.</param>
        /// <param name="comparer">An optional function to get a comparer for use in comparing values.</param>
        public DelegateRuleBuilder(Func<TParent, TValue> valueDelegate, string name, Func<IEqualityComparer<TValue>> comparer = null)
        {
            Delegate = valueDelegate ?? throw new ArgumentNullException(nameof(valueDelegate));
            Comparer = comparer ?? GetDefaultComparerFactory<TValue>();
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
