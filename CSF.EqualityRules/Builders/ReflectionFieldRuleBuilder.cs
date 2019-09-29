using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// A specialisation of <see cref="FieldRuleBuilder{TParent}"/> which uses reflection to directly get the value
    /// for a field.
    /// </summary>
    public class ReflectionFieldRuleBuilder<TParent> : FieldRuleBuilder<TParent>
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
            var fieldRule = new EqualityRule<object>(Comparer(), Name);
            var valueProvider = new ReflectionFieldValueProvider<TParent, object>(Field);
            var parentRule = new ParentEqualityRule<TParent, object>(valueProvider, fieldRule);
            return new[] { parentRule };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionFieldRuleBuilder{TParent}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="comparer">An optional factory which provides an equality comparer.</param>
        public ReflectionFieldRuleBuilder(FieldInfo field, Func<IEqualityComparer<object>> comparer = null) : base(field)
        {
            Comparer = comparer ?? GetDefaultComparerFactory<object>();
        }
    }
}
