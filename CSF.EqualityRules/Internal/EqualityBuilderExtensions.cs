using System;
namespace CSF.EqualityRules.Internal
{
    public static class EqualityBuilderExtensions
    {
        public static IProvidesRuleBuilders<T> AsRuleBuilderProvider<T>(this EqualityBuilder<T> builder) => builder;

        public static IResolvesServices GetServiceResolver<T>(this EqualityBuilder<T> builder)
        {
            return ((IProvidesResolver) builder).GetResolver();
        }
    }
}
