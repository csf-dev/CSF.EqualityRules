using System;

namespace CSF.EqualityRules.ValueProviders
{
    public class DelegateValueProvider<TParent,TValue> : IGetsValueFromParent<TParent,TValue>
    {
        readonly Func<TParent,TValue> getterDelegate;

        public TValue GetValue(TParent parent) => getterDelegate(parent);

        public DelegateValueProvider(Func<TParent, TValue> getterDelegate)
        {
            this.getterDelegate = getterDelegate ?? throw new ArgumentNullException(nameof(getterDelegate));
        }
    }
}