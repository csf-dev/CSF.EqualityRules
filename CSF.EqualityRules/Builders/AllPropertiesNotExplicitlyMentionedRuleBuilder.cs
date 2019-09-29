using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// A <see cref="RuleBuilder{T}"/> which adds all properties which are not explicitly mentioned by other rules.
    /// </summary>
    public class AllPropertiesNotExplicitlyMentionedRuleBuilder<TParent> : RuleBuilder<TParent>
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
            var properties = GetPropertiesNotMentionedByOtherBuilders(allBuilders);
            return properties
                .SelectMany(x => new ReflectionPropertyRuleBuilder<TParent>(x, Comparer).GetRules(allBuilders));
        }

        IEnumerable<PropertyInfo> GetPropertiesNotMentionedByOtherBuilders(IEnumerable<RuleBuilder<TParent>> allBuilders)
        {
            var propertiesExplicitlyMentioned = allBuilders
                .OfType<PropertyRuleBuilder<TParent>>()
                .Select(x => x.Property)
                .ToArray();
            var allProperties = typeof(TParent)
                .GetTypeInfo()
                .DeclaredProperties
                .Where(x => x.CanRead)
                .ToArray();

            return allProperties
                .Except(propertiesExplicitlyMentioned)
                .ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AllPropertiesNotExplicitlyMentionedRuleBuilder{TParent}"/> class.
        /// </summary>
        /// <param name="comparer">An optional equality comparer by which the properties will be compared.</param>
        public AllPropertiesNotExplicitlyMentionedRuleBuilder(Func<IEqualityComparer<object>> comparer = null)
        {
            Comparer = comparer ?? GetDefaultComparerFactory<object>();
        }
    }
}
