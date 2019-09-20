using System;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    public abstract class FieldRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        public FieldInfo Field { get; }

        protected FieldRuleBuilder(FieldInfo field)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
            Name = Field.Name;
        }
    }
}
