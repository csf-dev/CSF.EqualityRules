using System;

namespace CSF.EqualityRules
{
    public interface IResolvesServices
    {
        T Resolve<T>() where T : class;
        object Resolve(Type type);
    }
}