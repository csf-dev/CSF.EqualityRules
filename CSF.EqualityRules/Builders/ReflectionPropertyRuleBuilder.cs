using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// A specialisation of <see cref="PropertyRuleBuilder{TParent}"/> which uses reflection to directly get the value
    /// for a property.
    /// </summary>
    public class ReflectionPropertyRuleBuilder<TParent> : PropertyRuleBuilder<TParent>
    {
        /// <summary>
        /// Gets a function which will create a comparer for the generated rules.
        /// </summary>
        /// <value>The comparer.</value>
        public Func<IEqualityComparer<object>> Comparer { get; }

        /// <summary>
        /// Builds and returns a collection of <see cref="IEqualityRule{T}"/>.
        /// </summary>
        /// <returns>The rules.</returns>
        /// <param name="allBuilders">A collection of all rule-builders.</param>
        public override IEnumerable<IEqualityRule<TParent>> GetRules(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var propertyRule = new EqualityRule<object>(Comparer(), Name);
            var valueProvider = new ReflectionPropertyValueProvider<TParent, object>(Property);
            var parentRule = new ParentEqualityRule<TParent, object>(valueProvider, propertyRule);
            return new[] { parentRule };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionPropertyRuleBuilder{TParent}"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="comparer">An optional factory which provides an equality comparer.</param>
        public ReflectionPropertyRuleBuilder(PropertyInfo property, Func<IEqualityComparer<object>> comparer = null) : base(property)
        {
            Comparer = comparer ?? GetDefaultComparerFactory<object>();
        }
    }
}
