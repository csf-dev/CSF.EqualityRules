using System;
using CSF.EqualityRules.Builders;

namespace CSF.EqualityRules.Internal
{
    public static class EqualityBuilderExtensions
    {
        public static IProvidesRuleBuilders<T> AsRuleBuilderProvider<T>(this EqualityBuilder<T> builder) => builder;

        public static IResolvesServices GetServiceResolver<T>(this EqualityBuilder<T> builder)
        {
            return ((IProvidesResolver) builder).GetResolver();
        }

        public static ComparerFactoryBuilder<TCompared> GetComparerFactoryBuilder<TEquality,TCompared>(this EqualityBuilder<TEquality> builder)
        {
            var resolver = builder.GetServiceResolver();
            return new ComparerFactoryBuilder<TCompared>(resolver);
        }
    }
}
