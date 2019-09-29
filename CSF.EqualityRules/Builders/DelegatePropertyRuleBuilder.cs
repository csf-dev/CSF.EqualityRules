using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// A specialisation of <see cref="PropertyRuleBuilder{TParent}"/> which uses a getter-delegate to access the
    /// property value.
    /// </summary>
    public class DelegatePropertyRuleBuilder<TParent, TProperty> : PropertyRuleBuilder<TParent>
    {
        readonly Func<TParent, TProperty> getter;

        /// <summary>
        /// Gets a function which will create a comparer for the generated rules.
        /// </summary>
        /// <value>The comparer.</value>
        public Func<IEqualityComparer<TProperty>> Comparer { get; }

        /// <summary>
        /// Builds and returns a collection of <see cref="IEqualityRule{T}"/>.
        /// </summary>
        /// <returns>The rules.</returns>
        /// <param name="allBuilders">A collection of all rule-builders.</param>
        public override IEnumerable<IEqualityRule<TParent>> GetRules(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var propertyRule = new EqualityRule<TProperty>(Comparer(), Name);
            var valueProvider = new DelegateValueProvider<TParent, TProperty>(getter);
            var parentRule = new ParentEqualityRule<TParent, TProperty>(valueProvider, propertyRule);
            return new[] { parentRule };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatePropertyRuleBuilder{TParent, TProperty}"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="getter">The getter delegate.</param>
        /// <param name="comparer">An optional factory which provides an equality comparer.</param>
        public DelegatePropertyRuleBuilder(PropertyInfo property, Func<TParent, TProperty> getter, Func<IEqualityComparer<TProperty>> comparer = null) : base(property)
        {
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Comparer = comparer ?? GetDefaultComparerFactory<TProperty>();
        }
    }
}
