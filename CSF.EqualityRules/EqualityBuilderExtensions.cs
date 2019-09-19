using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSF.EqualityRules.Builders;
using CSF.Reflection;
using CSF.EqualityRules.Internal;
using System.Reflection;
using System.Collections;

namespace CSF.EqualityRules
{
    public static class EqualityBuilderExtensions
    {
        #region properties

        public static EqualityBuilder<TParent> ForProperty<TParent, TProp>(this EqualityBuilder<TParent> builder,
                                                                           Expression<Func<TParent, TProp>> prop,
                                                                           IEqualityComparer<TProp> comparer = null,
                                                                           string name = null)
        {
            var ruleBuilder = new DelegatePropertyRuleBuilder<TParent, TProp>(Reflect.Property(prop), prop.Compile(), comparer)
            {
                Name = name
            };
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        public static EqualityBuilder<TParent> ForProperty<TParent>(this EqualityBuilder<TParent> builder,
                                                                    PropertyInfo prop,
                                                                    IEqualityComparer comparer = null,
                                                                    string name = null)
        {
            var ruleBuilder = new ReflectionPropertyRuleBuilder<TParent>(prop, comparer)
            {
                Name = name
            };
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion

        #region fields

        public static EqualityBuilder<TParent> ForField<TParent, TField>(this EqualityBuilder<TParent> builder,
                                                                         Expression<Func<TParent, TField>> field,
                                                                         IEqualityComparer<TField> comparer = null,
                                                                         string name = null)
        {
            var ruleBuilder = new DelegateFieldRuleBuilder<TParent, TField>(Reflect.Field(field), field.Compile(), comparer)
            {
                Name = name
            };
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        public static EqualityBuilder<TParent> ForField<TParent>(this EqualityBuilder<TParent> builder,
                                                                 FieldInfo field,
                                                                 IEqualityComparer comparer = null,
                                                                 string name = null)
        {
            var ruleBuilder = new ReflectionFieldRuleBuilder<TParent>(field, comparer)
            {
                Name = name
            };
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion

        #region delegates

        public static EqualityBuilder<TParent> For<TParent, TValue>(this EqualityBuilder<TParent> builder,
                                                                    Func<TParent, TValue> dele,
                                                                    IEqualityComparer<TValue> comparer = null,
                                                                    string name = null)
        {
            var ruleBuilder = new DelegateRuleBuilder<TParent, TValue>(dele, comparer)
            {
                Name = name
            };
            var ruleProvider = builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion
    }
}
