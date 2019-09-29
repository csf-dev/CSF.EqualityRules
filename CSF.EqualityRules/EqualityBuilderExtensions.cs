using System;
using System.Linq.Expressions;
using System.Reflection;
using CSF.EqualityRules.Builders;
using CSF.Reflection;
using CSF.EqualityRules.Internal;

namespace CSF.EqualityRules
{
    /// <summary>
    /// Extension methods for adding equality rules.
    /// </summary>
    public static class EqualityBuilderExtensions
    {
        #region properties

        /// <summary>
        /// Adds an equality rule for a specific property.
        /// </summary>
        /// <returns>The equality builder.</returns>
        /// <param name="builder">The equality builder.</param>
        /// <param name="prop">An expression which identifies the property to be tested.</param>
        /// <param name="config">Configuration for how to test the property for equality.</param>
        /// <param name="name">An optional name for the equality rule.</param>
        /// <typeparam name="TParent">The object type being tested for equality.</typeparam>
        /// <typeparam name="TProp">The type of the property.</typeparam>
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

        /// <summary>
        /// Adds an equality rule for a specific property.
        /// </summary>
        /// <returns>The equality builder.</returns>
        /// <param name="builder">The equality builder.</param>
        /// <param name="prop">A <see cref="PropertyInfo"/> identifying the property.</param>
        /// <param name="config">Configuration for how to test the property for equality.</param>
        /// <param name="name">An optional name for the equality rule.</param>
        /// <typeparam name="TParent">The object type being tested for equality.</typeparam>
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

        /// <summary>
        /// Adds an equality rule which applies to all properties which are not explicitly mentioned by other rules.
        /// </summary>
        /// <returns>The equality builder.</returns>
        /// <param name="builder">The equality builder.</param>
        /// <param name="config">Configuration for how to test the properties for equality.</param>
        /// <typeparam name="TParent">The object type being tested for equality.</typeparam>
        public static EqualityBuilder<TParent> ForAllOtherProperties<TParent>(this EqualityBuilder<TParent> builder,
                                                                              Action<IBuildsComparerFactory<object>> config = null)
        {
            var comparerBuilder = builder.GetComparerFactoryBuilder<TParent,object>();
            config?.Invoke(comparerBuilder);
            var ruleBuilder = new AllPropertiesNotExplicitlyMentionedRuleBuilder<TParent>(comparerBuilder.Comparer);
            builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion

        #region fields

        /// <summary>
        /// Adds an equality rule for a specific field.
        /// </summary>
        /// <returns>The equality builder.</returns>
        /// <param name="builder">The equality builder.</param>
        /// <param name="field">An expression which identifies the field to be tested.</param>
        /// <param name="config">Configuration for how to test the field for equality.</param>
        /// <param name="name">An optional name for the equality rule.</param>
        /// <typeparam name="TParent">The object type being tested for equality.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
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

        /// <summary>
        /// Adds an equality rule for a specific field.
        /// </summary>
        /// <returns>The equality builder.</returns>
        /// <param name="builder">The equality builder.</param>
        /// <param name="field">A <see cref="FieldInfo"/> identifying the field.</param>
        /// <param name="config">Configuration for how to test the field for equality.</param>
        /// <param name="name">An optional name for the equality rule.</param>
        /// <typeparam name="TParent">The object type being tested for equality.</typeparam>
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

        /// <summary>
        /// Adds an equality rule which applies to all fields which are not explicitly mentioned by other rules.
        /// </summary>
        /// <returns>The equality builder.</returns>
        /// <param name="builder">The equality builder.</param>
        /// <param name="config">Configuration for how to test the fields for equality.</param>
        /// <typeparam name="TParent">The object type being tested for equality.</typeparam>
        public static EqualityBuilder<TParent> ForAllOtherFields<TParent>(this EqualityBuilder<TParent> builder,
                                                                          Action<IBuildsComparerFactory<object>> config = null)
        {
            var comparerBuilder = builder.GetComparerFactoryBuilder<TParent, object>();
            config?.Invoke(comparerBuilder);
            var ruleBuilder = new AllFieldsNotExplicitlyMentionedRuleBuilder<TParent>(comparerBuilder.Comparer);
            builder.AsRuleBuilderProvider().RuleBuilders.Add(ruleBuilder);
            return builder;
        }

        #endregion

        #region delegates

        /// <summary>
        /// Adds an equality rule for a custom delegate (such as a method access).
        /// </summary>
        /// <returns>The equality builder.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="name">Name.</param>
        /// <param name="dele">A custom delegate which gets a value from the object being tested for equality.</param>
        /// <param name="config">Config.</param>
        /// <typeparam name="TParent">The object type being tested for equality.</typeparam>
        /// <typeparam name="TValue">The type of the value retrieved by the delegate.</typeparam>
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
    }
}
