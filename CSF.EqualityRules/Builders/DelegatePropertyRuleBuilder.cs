using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public class DelegatePropertyRuleBuilder<TParent, TProperty> : PropertyRuleBuilder<TParent>
    {
        public Func<IEqualityComparer<TProperty>> Comparer { get; }
        readonly Func<TParent, TProperty> getter;

        public override IEnumerable<IEqualityRule<TParent>> GetRules()
        {
            var propertyRule = new EqualityRule<TProperty>(Comparer(), Name);
            var valueProvider = new DelegateValueProvider<TParent, TProperty>(getter);
            var parentRule = new ParentEqualityRule<TParent, TProperty>(valueProvider, propertyRule);
            return new[] { parentRule };
        }

        public DelegatePropertyRuleBuilder(PropertyInfo property, Func<TParent, TProperty> getter, Func<IEqualityComparer<TProperty>> comparer = null) : base(property)
        {
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Comparer = comparer ?? GetDefaultComparerFactory<TProperty>();
        }
    }
}
