using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public abstract class FieldRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        public FieldInfo Field { get; set; }

        protected FieldRuleBuilder(FieldInfo field)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
        }
    }

    public class FieldRuleBuilder<TParent,TField> : FieldRuleBuilder<TParent>
    {
        public IEqualityComparer<TField> Comparer { get; }

        public override IEnumerable<IEqualityRule<TParent>> GetRules()
        {
            var fieldRule = new EqualityRule<TField>(Comparer, Name);
            var valueProvider = new ReflectionFieldValueProvider<TParent, TField>(Field);
            var parentRule = new ParentEqualityRule<TParent, TField>(valueProvider, fieldRule);
            return new[] { parentRule };
        }

        public FieldRuleBuilder(FieldInfo field, IEqualityComparer<TField> comparer = null) : base(field)
        {
            Comparer = comparer ?? EqualityComparer<TField>.Default;
        }
    }
}
