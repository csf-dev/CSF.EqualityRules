using System;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// Base type for a <see cref="RuleBuilder{T}"/> which builds rules for a field.
    /// </summary>
    public abstract class FieldRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        /// <summary>
        /// Gets the field to which this builder applies.
        /// </summary>
        /// <value>The field.</value>
        public FieldInfo Field { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldRuleBuilder{TParent}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        protected FieldRuleBuilder(FieldInfo field)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
            Name = Field.Name;
        }
    }
}
