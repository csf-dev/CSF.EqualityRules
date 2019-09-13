using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public abstract class PropertyRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        public PropertyInfo Property { get; }

        protected PropertyRuleBuilder(PropertyInfo property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
        }
    }

    public class PropertyRuleBuilder<TParent,TProperty> : PropertyRuleBuilder<TParent>
    {
        public IEqualityComparer<TProperty> Comparer { get; }

        public override IEnumerable<IEqualityRule<TParent>> GetRules()
        {
            var propertyRule = new EqualityRule<TProperty>(Comparer, Name);
            var valueProvider = new ReflectionPropertyValueProvider<TParent, TProperty>(Property);
            var parentRule = new ParentEqualityRule<TParent, TProperty>(valueProvider, propertyRule);
            return new[] { parentRule };
        }

        public PropertyRuleBuilder(PropertyInfo property, IEqualityComparer<TProperty> comparer = null) : base(property)
        {
            Comparer = comparer ?? EqualityComparer<TProperty>.Default;
        }
    }
}
