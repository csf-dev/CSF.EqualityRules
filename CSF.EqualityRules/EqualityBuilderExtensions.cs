using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSF.EqualityRules.Builders;
using CSF.Reflection;

namespace CSF.EqualityRules
{
    public static class EqualityBuilderExtensions
    {
        public static IBuildsEqualityRules<T> AsRuleBuilderProvider<T>(this EqualityBuilder<T> builder) => builder;

        public static EqualityBuilder<TParent> ForProperty<TParent,TProp>(this EqualityBuilder<TParent> builder,
                                                                          Expression<Func<TParent,TProp>> expr,
                                                                          IEqualityComparer<TProp> comparer = null,
                                                                          string name = null)
        {
            var property = Reflect.Property(expr);
            var ruleBuilder = new PropertyRuleBuilder<TParent, TProp>(property, comparer) { Name = name };
            builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        public static EqualityBuilder<TParent> ForField<TParent,TField>(this EqualityBuilder<TParent> builder,
            Expression<Func<TParent,TField>> expr,
            IEqualityComparer<TField> comparer = null,
            string name = null)
        {
            var field = Reflect.Field(expr);
            var ruleBuilder = new FieldRuleBuilder<TParent, TField>(field, comparer) { Name = name };
            builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        public static EqualityBuilder<TParent> For<TParent,TReturn>(this EqualityBuilder<TParent> builder,
            Func<TParent,TReturn> func,
            IEqualityComparer<TReturn> comparer = null,
            string name = null)
        {
            var ruleBuilder = new DelegateRuleBuilder<TParent, TReturn>(func, comparer) { Name = name };
            builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }
    }
}
