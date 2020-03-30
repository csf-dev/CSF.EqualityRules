using System.Collections.Generic;
using System.Linq;
using CSF.EqualityRules.Builders;
using CSF.EqualityRules.Internal;
using CSF.EqualityRules.Rules;

namespace CSF.EqualityRules
{
    /// <summary>
    /// A builder class which creates an instance of <see cref="IGetsEqualityResult{T}"/> (which itself implements
    /// <see cref="IEqualityComparer{T}"/>) from a number of fluently-defined rules.
    /// </summary>
    /// <typeparam name="T">The type for which equality should be tested.</typeparam>
    public class EqualityBuilder<T> : IProvidesRuleBuilders<T>, IProvidesResolver
    {
        readonly ISet<RuleBuilder<T>> ruleBuilders;
        readonly IResolvesServices resolver;

        ISet<RuleBuilder<T>> IProvidesRuleBuilders<T>.RuleBuilders => ruleBuilders;

        IResolvesServices IProvidesResolver.GetResolver() => resolver;

        /// <summary>
        /// Builds the equality comparer and returns it.
        /// </summary>
        /// <returns>The equality comparer.</returns>
        public IGetsEqualityResult<T> Build()
        {
            var rules = ruleBuilders.SelectMany(x => x.GetRules(ruleBuilders));
            return new MultipleEqualityRuleRunner<T>(rules);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityBuilder{T}"/> class.
        /// </summary>
        /// <param name="resolver">An optional object which will be used to resolve dependencies.</param>
        public EqualityBuilder(IResolvesServices resolver = null)
        {
            ruleBuilders = new HashSet<RuleBuilder<T>>();
            this.resolver = resolver ?? new ActivatorServiceResolver();
        }
    }

    /// <summary>
    /// Static functionality related to an equality builder.
    /// </summary>
    public static class EqualityBuilder
    {
        /// <summary>
        /// A convenience method which creates an equality tester based upon a specified equality comparer.
        /// The rule will have a single rule named "Default", which uses the comparer.
        /// </summary>
        /// <returns>The equality tester.</returns>
        /// <param name="comparer">An equality comparer to convert to an equality tester.</param>
        /// <typeparam name="T">The tested type.</typeparam>
        public static IGetsEqualityResult<T> From<T>(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
                throw new System.ArgumentNullException(nameof(comparer));

            return new EqualityBuilder<T>()
                .For("Default", x => x, c => c.UsingComparer(comparer))
                .Build();
        }
    }
}