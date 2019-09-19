using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public class DelegateFieldRuleBuilder<TParent, TField> : FieldRuleBuilder<TParent>
    {
        public IEqualityComparer<TField> Comparer { get; }
        readonly Func<TParent, TField> getter;

        public override IEnumerable<IEqualityRule<TParent>> GetRules()
        {
            var fieldRule = new EqualityRule<TField>(Comparer, Name);
            var valueProvider = new DelegateValueProvider<TParent, TField>(getter);
            var parentRule = new ParentEqualityRule<TParent, TField>(valueProvider, fieldRule);
            return new[] { parentRule };
        }

        public DelegateFieldRuleBuilder(FieldInfo field, Func<TParent, TField> getter, IEqualityComparer<TField> comparer = null) : base(field)
        {
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Comparer = comparer ?? EqualityComparer<TField>.Default;
        }
    }
}
