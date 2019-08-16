namespace CSF.EqualityRules
{
    public interface IGetsEqualityResult<in T> : IEqualityRule<T>
    {
        EqualityResult GetEqualityResult(T obj);
    }
}