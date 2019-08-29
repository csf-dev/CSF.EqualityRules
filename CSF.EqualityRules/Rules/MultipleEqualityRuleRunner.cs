using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.EqualityRules.Rules
{
    public class MultipleEqualityRuleRunner<T> : IEqualityRule<T>
    {
        const int Prime1 = 17, Prime2 = 31;

        readonly IReadOnlyCollection<IEqualityRule<T>> rules;
        readonly EqualityResultFactory resultFactory;

        public string Name => null;

        public bool Equals(T x, T y) => GetEqualityResult(x, y).AreEqual;

        public int GetHashCode(T obj)
        {
            unchecked
            {
                return rules.Aggregate(Prime1, (acc, next) => acc * Prime2 + next.GetHashCode(obj));
            }
        }

        public EqualityResult GetEqualityResult(T x, T y)
        {
            var ruleResults = rules
                .SelectMany(rule => rule.GetEqualityResult(x, y)?.RuleResults)
                .ToArray();
            return resultFactory.GetEqualityResult(ruleResults);
        }

        public MultipleEqualityRuleRunner(IEnumerable<IEqualityRule<T>> rules)
        {
            this.rules = rules?.ToArray() ?? throw new ArgumentNullException(nameof(rules));

            resultFactory = new EqualityResultFactory();
        }
    }
}