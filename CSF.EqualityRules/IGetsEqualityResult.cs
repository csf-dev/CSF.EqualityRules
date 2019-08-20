using System.Collections.Generic;

namespace CSF.EqualityRules
{
    public interface IGetsEqualityResult<in T> :  IEqualityComparer<T>
    {
        EqualityResult GetEqualityResult(T obj);
    }
}