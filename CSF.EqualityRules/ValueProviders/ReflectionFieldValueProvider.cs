using System;
using System.Reflection;

namespace CSF.EqualityRules.ValueProviders
{
    /// <summary>
    /// Implementation of <see cref="IGetsValueFromParent{TParent, TValue}"/> which uses reflection to get a
    /// value from a field.
    /// </summary>
    public class ReflectionFieldValueProvider<TParent,TValue> : IGetsValueFromParent<TParent,TValue>
    {
        readonly IGetsValueFromParent<TParent, TValue> provider;

        /// <summary>
        /// Gets the 'child' value from the parent object.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="parent">Parent.</param>
        public TValue GetValue(TParent parent) => provider.GetValue(parent);

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ReflectionFieldValueProvider{TParent, TValue}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        public ReflectionFieldValueProvider(FieldInfo field)
        {
            if(field == null) throw new ArgumentNullException(nameof(field));
            provider = new DelegateValueProvider<TParent,TValue>(parent => (TValue) field.GetValue(parent));
        }
    }
}