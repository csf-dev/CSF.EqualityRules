using System;

namespace CSF.EqualityRules.ValueProviders
{
    /// <summary>
    /// Implementation of <see cref="IGetsValueFromParent{TParent, TValue}"/> using a delegate function.
    /// </summary>
    public class DelegateValueProvider<TParent,TValue> : IGetsValueFromParent<TParent,TValue>
    {
        readonly Func<TParent,TValue> getterDelegate;

        /// <summary>
        /// Gets the 'child' value from the parent object.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="parent">Parent.</param>
        public TValue GetValue(TParent parent) => getterDelegate(parent);

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateValueProvider{TParent, TValue}"/> class.
        /// </summary>
        /// <param name="getterDelegate">Getter delegate.</param>
        public DelegateValueProvider(Func<TParent, TValue> getterDelegate)
        {
            this.getterDelegate = getterDelegate ?? throw new ArgumentNullException(nameof(getterDelegate));
        }
    }
}