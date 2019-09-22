using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    public class AllPropertiesNotExplicitlyMentionedRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        public Func<IEqualityComparer<object>> Comparer { get; }

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

        public AllPropertiesNotExplicitlyMentionedRuleBuilder(Func<IEqualityComparer<object>> comparer = null)
        {
            Comparer = comparer ?? GetDefaultComparerFactory<object>();
        }
    }
}
