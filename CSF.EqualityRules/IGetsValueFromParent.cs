namespace CSF.EqualityRules
{
    public interface IGetsValueFromParent<in TParent, out TValue>
    {
        TValue GetValue(TParent parent);
    }
}