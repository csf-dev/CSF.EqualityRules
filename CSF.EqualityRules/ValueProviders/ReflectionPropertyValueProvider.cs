using System;
using System.Reflection;

namespace CSF.EqualityRules.ValueProviders
{
    public class ReflectionPropertyValueProvider<TParent,TValue> : IGetsValueFromParent<TParent,TValue>
    {
        readonly IGetsValueFromParent<TParent, TValue> provider;

        public TValue GetValue(TParent parent) => provider.GetValue(parent);

        /// <summary>
        /// Helper method to perform delegate caching in order to optimize retrieving values from the parent.
        /// </summary>
        /// <param name="property">The property info</param>
        /// <returns>A delegate which gets the value</returns>
        static Func<TParent,TValue> GetGetterDelegate(PropertyInfo property)
        {
            var getMethod = property.GetMethod;
            var getter = getMethod.CreateDelegate(typeof(Func<TParent,TValue>));
            return (Func<TParent,TValue>) getter;
        }

        public ReflectionPropertyValueProvider(PropertyInfo property)
        {
            if(property == null) throw new ArgumentNullException(nameof(property));
            provider = new DelegateValueProvider<TParent,TValue>(GetGetterDelegate(property));
        }
    }
}