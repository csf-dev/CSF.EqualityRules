using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// A specialisation of <see cref="FieldRuleBuilder{TParent}"/> which uses a getter-delegate to access the
    /// field value.
    /// </summary>
    public class DelegateFieldRuleBuilder<TParent, TField> : FieldRuleBuilder<TParent>
    {
        readonly Func<TParent, TField> getter;

        /// <summary>
        /// Gets a function which will create a comparer for the generated rules.
        /// </summary>
        /// <value>The comparer.</value>
        public Func<IEqualityComparer<TField>> Comparer { get; }

        /// <summary>
        /// Builds and returns a collection of <see cref="IEqualityRule{T}"/>.
        /// </summary>
        /// <returns>The rules.</returns>
        /// <param name="allBuilders">A collection of all rule-builders.</param>
        public override IEnumerable<IEqualityRule<TParent>> GetRules(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var fieldRule = new EqualityRule<TField>(Comparer(), Name);
            var valueProvider = new DelegateValueProvider<TParent, TField>(getter);
            var parentRule = new ParentEqualityRule<TParent, TField>(valueProvider, fieldRule);
            return new[] { parentRule };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateFieldRuleBuilder{TParent, TField}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="getter">The getter delegate.</param>
        /// <param name="comparer">An optional factory which provides an equality comparer.</param>
        public DelegateFieldRuleBuilder(FieldInfo field, Func<TParent, TField> getter, Func<IEqualityComparer<TField>> comparer = null) : base(field)
        {
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Comparer = comparer ?? GetDefaultComparerFactory<TField>();
        }
    }
}
