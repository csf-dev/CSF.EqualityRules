using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public abstract class PropertyRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        public PropertyInfo Property { get; set; }

        protected PropertyRuleBuilder(PropertyInfo property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
        }
    }
}
