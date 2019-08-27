using System;
using System.Collections.Generic;
using System.Linq;
using CSF.EqualityRules.Internal;

namespace CSF.EqualityRules.Rules
{
    public class MultipleEqualityRuleRunner<T> : IGetsEqualityResult<T>
    {
        const int Prime1 = 17, Prime2 = 31;

        readonly IReadOnlyCollection<IEqualityRule<T>> rules;
        readonly IGetsEqualityResultFromRuleResults resultFactory;
        readonly IGetsEqualityRuleResult ruleResultFactory;

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
            var ruleResults = rules.Select(rule => ruleResultFactory.GetResult(rule, x, y));
            return resultFactory.GetEqualityResult(ruleResults);
        }

        public MultipleEqualityRuleRunner(IEnumerable<IEqualityRule<T>> rules,
                                          IGetsEqualityResultFromRuleResults resultFactory = null,
                                          IGetsEqualityRuleResult ruleResultFactory = null)
        {
            this.resultFactory = resultFactory ?? new EqualityResultFactory();
            this.ruleResultFactory = ruleResultFactory ?? new EqualityRuleResultFactory();
            this.rules = rules?.ToArray() ?? throw new ArgumentNullException(nameof(rules));
        }
    }
}