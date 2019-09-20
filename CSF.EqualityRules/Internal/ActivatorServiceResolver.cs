using System;

namespace CSF.EqualityRules.Internal
{
    public class ActivatorServiceResolver : IResolvesServices
    {
        public T Resolve<T>() where T : class => Activator.CreateInstance<T>();
        public object Resolve(Type type) => Activator.CreateInstance(type);
    }
}