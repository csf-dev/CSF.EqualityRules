using System.Collections.Generic;
using CSF.EqualityRules.Builders;
using CSF.EqualityRules.Internal;

namespace CSF.EqualityRules
{
    public static class EqualityBuilder
    {
        public static EqualityBuilder<T> ForType<T>() => new EqualityBuilder<T>();
    }

    public class EqualityBuilder<T> : IProvidesRuleBuilders<T>, IProvidesResolver
    {
        readonly ISet<RuleBuilder<T>> ruleBuilders;
        readonly IResolvesServices resolver;

        ISet<RuleBuilder<T>> IProvidesRuleBuilders<T>.RuleBuilders => ruleBuilders;

        IResolvesServices IProvidesResolver.GetResolver() => resolver;

        public EqualityBuilder(IResolvesServices resolver = null)
        {
            ruleBuilders = new HashSet<RuleBuilder<T>>();
            this.resolver = resolver ?? new ActivatorServiceResolver();
        }
    }
}