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
        public IEqualityComparer<object> Comparer { get; }

        public override IEnumerable<IEqualityRule<TParent>> GetRules()
        {
            var propertyRule = new EqualityRule<object>(Comparer, Name);
            var valueProvider = new ReflectionPropertyValueProvider<TParent, object>(Property);
            var parentRule = new ParentEqualityRule<TParent, object>(valueProvider, propertyRule);
            return new[] { parentRule };
        }

        public ReflectionPropertyRuleBuilder(PropertyInfo property, IEqualityComparer comparer = null) : base(property)
        {
            var genericComparer = new NonGenericEqualityComparerAdapter<object>(comparer ?? EqualityComparer<object>.Default);
            Comparer = genericComparer;
        }
    }
}
