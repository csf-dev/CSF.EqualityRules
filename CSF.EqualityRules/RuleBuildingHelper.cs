using System.Collections.Generic;
using System.Reflection;

namespace CSF.EqualityRules
{
    public class RuleBuildingHelper<T> : IBuildsEqualityRule<T>
    {
        readonly ISet<PropertyInfo> properties;
        readonly ISet<FieldInfo> fields;
        readonly ICollection<IEqualityRule<T>> rules;

        ISet<PropertyInfo> IBuildsEqualityRule<T>.Properties => properties;

        ISet<FieldInfo> IBuildsEqualityRule<T>.Fields => fields;

        ICollection<IEqualityRule<T>> IBuildsEqualityRule<T>.Rules => rules;

        public RuleBuildingHelper()
        {
            properties = new HashSet<PropertyInfo>();
            fields = new HashSet<FieldInfo>();
            rules = new List<IEqualityRule<T>>();
        }
    }
}