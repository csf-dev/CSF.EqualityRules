namespace CSF.EqualityRules.ValueProviders
{
    public interface IGetsValueFromParent<in TParent, out TValue>
    {
        TValue GetValue(TParent parent);
    }
}