using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public class ReflectionFieldRuleBuilder<TParent> : FieldRuleBuilder<TParent>
    {
        public IEqualityComparer<object> Comparer { get; }

        public override IEnumerable<IEqualityRule<TParent>> GetRules()
        {
            var fieldRule = new EqualityRule<object>(Comparer, Name);
            var valueProvider = new ReflectionFieldValueProvider<TParent, object>(Field);
            var parentRule = new ParentEqualityRule<TParent, object>(valueProvider, fieldRule);
            return new[] { parentRule };
        }

        public ReflectionFieldRuleBuilder(FieldInfo field, IEqualityComparer comparer = null) : base(field)
        {
            var genericComparer = new NonGenericEqualityComparerAdapter<object>(comparer ?? EqualityComparer<object>.Default);
            Comparer = genericComparer;
        }
    }
}
