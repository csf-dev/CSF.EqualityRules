using System;

namespace CSF.EqualityRules.Internal
{
    public class EqualityRuleResultFactory : IGetsEqualityRuleResult
    {
        public EqualityRuleResult GetResult<T>(IEqualityRule<T> rule, T x, T y)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            var builder = new EqualityRuleResultBuilder();
            return builder.GetRuleResult(rule, x, y);
        }
    }
}