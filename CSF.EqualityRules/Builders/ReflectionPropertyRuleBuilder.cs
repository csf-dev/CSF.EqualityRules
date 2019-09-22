using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public class ReflectionPropertyRuleBuilder<TParent> : PropertyRuleBuilder<TParent>
    {
        public Func<IEqualityComparer<object>> Comparer { get; }

        public override IEnumerable<IEqualityRule<TParent>> GetRules(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var propertyRule = new EqualityRule<object>(Comparer(), Name);
            var valueProvider = new ReflectionPropertyValueProvider<TParent, object>(Property);
            var parentRule = new ParentEqualityRule<TParent, object>(valueProvider, propertyRule);
            return new[] { parentRule };
        }

        public ReflectionPropertyRuleBuilder(PropertyInfo property, Func<IEqualityComparer<object>> comparer = null) : base(property)
        {
            Comparer = comparer ?? GetDefaultComparerFactory<object>();
        }
    }
}
