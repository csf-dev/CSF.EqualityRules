using System;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// Base type for a <see cref="RuleBuilder{T}"/> which builds rules for a property.
    /// </summary>
    public abstract class PropertyRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        /// <summary>
        /// Gets the property to which this builder applies.
        /// </summary>
        /// <value>The property.</value>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyRuleBuilder{TParent}"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        protected PropertyRuleBuilder(PropertyInfo property)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Name = Property.Name;
        }
    }
}
