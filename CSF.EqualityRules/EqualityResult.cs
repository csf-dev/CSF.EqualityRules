using System;
using System.Collections.Generic;

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
    }
}