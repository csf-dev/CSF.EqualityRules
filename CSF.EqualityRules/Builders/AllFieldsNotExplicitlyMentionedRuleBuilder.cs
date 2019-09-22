using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSF.EqualityRules.Builders
{
    public class AllFieldsNotExplicitlyMentionedRuleBuilder<TParent> : RuleBuilder<TParent>
    {
        public Func<IEqualityComparer<object>> Comparer { get; }

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

        public AllFieldsNotExplicitlyMentionedRuleBuilder(Func<IEqualityComparer<object>> comparer = null)
        {
            Comparer = comparer ?? GetDefaultComparerFactory<object>();
        }
    }
}
