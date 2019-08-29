using System.Collections.Generic;
using System.Reflection;

namespace CSF.EqualityRules
{
    public interface IBuildsEqualityRule<T>
    {
        ISet<PropertyInfo> Properties { get; }
        ISet<FieldInfo> Fields { get; }
        ICollection<IEqualityRule<T>> Rules { get; }
    }
}