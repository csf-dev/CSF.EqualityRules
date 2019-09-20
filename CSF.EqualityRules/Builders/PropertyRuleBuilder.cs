using System;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    public abstract class PropertyRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        public PropertyInfo Property { get; }

        protected PropertyRuleBuilder(PropertyInfo property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Name = Property.Name;
        }
    }
}
