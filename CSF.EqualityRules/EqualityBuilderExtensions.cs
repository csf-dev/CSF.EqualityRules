using System;
using System.Linq.Expressions;
using CSF.EqualityRules.Builders;
using CSF.Reflection;
using CSF.EqualityRules.Internal;
using System.Reflection;
using System.Linq;
using CSF.EqualityRules.Rules;

namespace CSF.EqualityRules
{
    public static class EqualityBuilderExtensions
    {
        #region properties

        public static EqualityBuilder<TParent> ForProperty<TParent, TProp>(this EqualityBuilder<TParent> builder,
                                                                           Expression<Func<TParent, TProp>> prop,
                                                                           Action<IBuildsComparerFactory<TProp>> config = null,
                                                                           string name = null)
        {
            var comparerBuilder = builder.GetComparerFactoryBuilder<TParent, TProp>();
            config?.Invoke(comparerBuilder);
            var ruleBuilder = new DelegatePropertyRuleBuilder<TParent, TProp>(Reflect.Property(prop),
                                                                              prop.Compile(),
                                                                              comparerBuilder.Comparer);
            if (name != null) ruleBuilder.Name = name;
            builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        public static EqualityBuilder<TParent> ForProperty<TParent>(this EqualityBuilder<TParent> builder,
                                                                    PropertyInfo prop,
                                                                    Action<IBuildsComparerFactory<object>> config = null,
                                                                    string name = null)
        {
            var comparerBuilder = builder.GetComparerFactoryBuilder<TParent, object>();
            config?.Invoke(comparerBuilder);
            var ruleBuilder = new ReflectionPropertyRuleBuilder<TParent>(prop, comparerBuilder.Comparer);
            if (name != null) ruleBuilder.Name = name;
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion

        #region fields

        public static EqualityBuilder<TParent> ForField<TParent, TField>(this EqualityBuilder<TParent> builder,
                                                                         Expression<Func<TParent, TField>> field,
                                                                         Action<IBuildsComparerFactory<TField>> config = null,
                                                                         string name = null)
        {
            var comparerBuilder = builder.GetComparerFactoryBuilder<TParent, TField>();
            config?.Invoke(comparerBuilder);
            var ruleBuilder = new DelegateFieldRuleBuilder<TParent, TField>(Reflect.Field(field),
                                                                            field.Compile(),
                                                                            comparerBuilder.Comparer);
            if (name != null) ruleBuilder.Name = name;
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        public static EqualityBuilder<TParent> ForField<TParent>(this EqualityBuilder<TParent> builder,
                                                                 FieldInfo field,
                                                                 Action<IBuildsComparerFactory<object>> config = null,
                                                                 string name = null)
        {
            var comparerBuilder = builder.GetComparerFactoryBuilder<TParent, object>();
            config?.Invoke(comparerBuilder);
            var ruleBuilder = new ReflectionFieldRuleBuilder<TParent>(field, comparerBuilder.Comparer);
            if (name != null) ruleBuilder.Name = name;
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion

        #region delegates

        public static EqualityBuilder<TParent> For<TParent, TValue>(this EqualityBuilder<TParent> builder,
                                                                    string name,
                                                                    Func<TParent, TValue> dele,
                                                                    Action<IBuildsComparerFactory<TValue>> config = null)
        {
            var comparerBuilder = builder.GetComparerFactoryBuilder<TParent, TValue>();
            config?.Invoke(comparerBuilder);
            var ruleBuilder = new DelegateRuleBuilder<TParent, TValue>(dele, name, comparerBuilder.Comparer);
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion

        #region building the comparer

        public static IGetsEqualityResult<T> Build<T>(this EqualityBuilder<T> builder)
        {
            var ruleProvider = builder.AsRuleBuilderProvider();
            var rules = ruleProvider.RuleBuilders.SelectMany(x => x.GetRules());
            return new MultipleEqualityRuleRunner<T>(rules);
        }

        #endregion
    }
}
