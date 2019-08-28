using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.EqualityRules
{
    public class EqualityResult
    {
        public bool AreEqual { get; }

        public IReadOnlyCollection<EqualityRuleResult> RuleResults { get; }

        public EqualityResult(bool areEqual, IReadOnlyCollection<EqualityRuleResult> ruleResults)
        {
            AreEqual = areEqual;
            RuleResults = ruleResults ?? throw new ArgumentNullException(nameof(ruleResults));
        }

        static EqualityResult()
        {
            Equal = new EqualityResult(true, Enumerable.Empty<EqualityRuleResult>().ToArray());
        }

        /// <summary>
        /// Gets a singleton instance indicating equality.
        /// </summary>
        /// <value>The result instance.</value>
        public static EqualityResult Equal { get; }
    }
}