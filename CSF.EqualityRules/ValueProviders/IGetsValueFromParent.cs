namespace CSF.EqualityRules.ValueProviders
{
    /// <summary>
    /// Describes a service which can get a value from a parent object.
    /// </summary>
    public interface IGetsValueFromParent<in TParent, out TValue>
    {
        /// <summary>
        /// Gets the 'child' value from the parent object.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="parent">Parent.</param>
        TValue GetValue(TParent parent);
    }
}