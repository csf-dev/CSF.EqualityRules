using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Builders
{
    public interface IBuildsRule
    {
        string Name { get; set; }
        IEnumerable<IEqualityRule> GetRules(IEnumerable<IBuildsRule> allBuilders = null);
    }
}
