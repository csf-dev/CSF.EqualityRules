using System.Collections.Generic;

namespace CSF.EqualityRules.Internal
{
    public interface IGetsEqualityResultFromRuleResults
    {
        EqualityResult GetEqualityResult(params IEnumerable<EqualityRuleResult>[] ruleResults);
    }
}