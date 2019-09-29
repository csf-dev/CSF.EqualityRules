using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// A <see cref="RuleBuilder{T}"/> which adds all fields which are not explicitly mentioned by other rules.
    /// </summary>
    public class AllFieldsNotExplicitlyMentionedRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        /// <summary>
        /// Gets the comparer which has been chosen for the rules built by this instance.
        /// </summary>
        /// <value>The comparer.</value>
        public Func<IEqualityComparer<object>> Comparer { get; }

        /// <summary>
        /// Builds and returns a collection of <see cref="IEqualityRule{T}"/>.
        /// </summary>
        /// <returns>The rules.</returns>
        /// <param name="allBuilders">A collection of all rule-builders.</param>
        public override IEnumerable<IEqualityRule<TParent>> GetRules(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var fields = GetFieldsNotMentionedByOtherBuilders(allBuilders);
            return fields
                .SelectMany(x => new ReflectionFieldRuleBuilder<TParent>(x, Comparer).GetRules(allBuilders));
        }

        IEnumerable<FieldInfo> GetFieldsNotMentionedByOtherBuilders(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var fieldsExplicitlyMentioned = allBuilders
                .OfType<FieldRuleBuilder<TParent>>()
                .Select(x => x.Field)
                .ToArray();
            var allFields = typeof(TParent)
                .GetTypeInfo()
                .DeclaredFields
                .ToArray();

            return allFields
                .Except(fieldsExplicitlyMentioned)
                .ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AllFieldsNotExplicitlyMentionedRuleBuilder{TParent}"/> class.
        /// </summary>
        /// <param name="comparer">An optional equality comparer by which the fields will be compared.</param>
        public AllFieldsNotExplicitlyMentionedRuleBuilder(Func<IEqualityComparer<object>> comparer = null)
        {
            Comparer = comparer ?? GetDefaultComparerFactory<object>();
        }
    }
}
