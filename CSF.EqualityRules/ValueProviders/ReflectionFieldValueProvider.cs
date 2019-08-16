using System;
using System.Reflection;

namespace CSF.EqualityRules.ValueProviders
{
    public class ReflectionFieldValueProvider<TParent,TValue> : IGetsValueFromParent<TParent,TValue>
    {
        readonly IGetsValueFromParent<TParent, TValue> provider;

        public TValue GetValue(TParent parent) => provider.GetValue(parent);

        public ReflectionFieldValueProvider(FieldInfo field)
        {
            if(field == null) throw new ArgumentNullException(nameof(field));
            provider = new DelegateValueProvider<TParent,TValue>(parent => (TValue) field.GetValue(parent));
        }
    }
}