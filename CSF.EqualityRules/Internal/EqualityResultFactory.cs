using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.EqualityRules.Internal
{
    public class EqualityResultFactory : IGetsEqualityResultFromRuleResults
    {
        public EqualityResult GetEqualityResult(params IEnumerable<EqualityRuleResult>[] ruleResults)
        {
            var allRuleResults = ruleResults.SelectMany(x => x).ToArray();
            AssertNoDuplicateRules(allRuleResults);
            return new EqualityResult(allRuleResults.All(x => x.Passed), allRuleResults);
        }

        void AssertNoDuplicateRules(IEnumerable<EqualityRuleResult> results)
        {
            if(results.GroupBy(x => x.Name).Any(x => x.Count() > 1))
                throw new ArgumentException("There must be no duplicate rule results", nameof(results));
        }
    }
}