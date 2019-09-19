using System.Collections.Generic;
using CSF.EqualityRules.Builders;

namespace CSF.EqualityRules
{
    public static class EqualityBuilder
    {
        public static EqualityBuilder<T> ForType<T>() => new EqualityBuilder<T>();
    }

    public class EqualityBuilder<T> : IProvidesRuleBuilders<T>
    {
        readonly ISet<RuleBuilder<T>> ruleBuilders;

        ISet<RuleBuilder<T>> IProvidesRuleBuilders<T>.RuleBuilders => ruleBuilders;

        public EqualityBuilder()
        {
            ruleBuilders = new HashSet<RuleBuilder<T>>();
        }
    }
}