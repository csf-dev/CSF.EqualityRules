using System;
using System.Reflection;
using CSF.Reflection;

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
        Func<TParent,TValue> GetGetterDelegate(PropertyInfo property)
        {
            var getGetterMethodDef = Reflect.Method<ReflectionPropertyValueProvider<TParent, TValue>>(x => x.GetObjectGetterDelegate<object>(property));
            var getGetterMethod = (Func<PropertyInfo, Func<TParent, object>>) getGetterMethodDef
                .GetGenericMethodDefinition()
                .MakeGenericMethod(property.PropertyType)
                .CreateDelegate(typeof(Func<PropertyInfo,Func<TParent,object>>), this);

            return parent => (TValue) (getGetterMethod(property)(parent));
        }

        Func<TParent, object> GetObjectGetterDelegate<T>(PropertyInfo property)
        {
            var getMethod = property.GetMethod;
            var getter = (Func<TParent, T>) getMethod.CreateDelegate(typeof(Func<TParent, T>));
            return p => getter(p);
        }

        public ReflectionPropertyValueProvider(PropertyInfo property)
        {
            if(property == null) throw new ArgumentNullException(nameof(property));
            provider = new DelegateValueProvider<TParent,TValue>(GetGetterDelegate(property));
        }
    }
}