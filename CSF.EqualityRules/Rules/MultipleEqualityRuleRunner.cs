using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.EqualityRules.Rules
{
    /// <summary>
    /// Implementation of an equality rule which can run many 'child' equality rules and aggregate their results.
    /// </summary>
    public class MultipleEqualityRuleRunner<T> : IEqualityRule<T>
    {
        const int Prime1 = 17, Prime2 = 31;

        readonly IReadOnlyCollection<IEqualityRule<T>> rules;
        readonly EqualityResultFactory resultFactory;

        /// <summary>
        /// Gets the rule name.
        /// </summary>
        /// <value>The rule name.</value>
        public string Name => null;

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <exception cref="T:System.ArgumentException" />
        /// <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other.
        public bool Equals(T x, T y) => GetEqualityResult(x, y).AreEqual;

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <returns>A hash code for the specified object.</returns>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
        public int GetHashCode(T obj)
        {
            unchecked
            {
                return rules.Aggregate(Prime1, (acc, next) => acc * Prime2 + next.GetHashCode(obj));
            }
        }

        /// <summary>
        /// Determines whether the specified objects are equal, running all 'child' rules and aggregating/collating
        /// thei results.
        /// </summary>
        /// <returns>An equality result.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public EqualityResult GetEqualityResult(T x, T y)
        {
            var ruleResults = rules
                .SelectMany(rule => rule.GetEqualityResult(x, y)?.RuleResults)
                .ToArray();
            return resultFactory.GetEqualityResult(ruleResults);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleEqualityRuleRunner{T}"/> class.
        /// </summary>
        /// <param name="rules">The child rules.</param>
        public MultipleEqualityRuleRunner(IEnumerable<IEqualityRule<T>> rules)
        {
            this.rules = rules?.ToArray() ?? throw new ArgumentNullException(nameof(rules));

            resultFactory = new EqualityResultFactory();
        }
    }
}