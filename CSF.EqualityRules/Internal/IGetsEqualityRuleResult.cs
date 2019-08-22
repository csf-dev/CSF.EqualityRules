namespace CSF.EqualityRules.Internal
{
    public interface IGetsEqualityRuleResult
    {
        EqualityRuleResult GetResult<T>(IEqualityRule<T> rule, T x, T y);
    }
}