using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// Describes a service which builds a collection of equality rules.
    /// </summary>
    public interface IBuildsRule
    {
        /// <summary>
        /// Gets or sets the name for this rule-builder.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Builds and returns a collection of <see cref="IEqualityRule"/>.
        /// </summary>
        /// <returns>The rules.</returns>
        /// <param name="allBuilders">A collection of all rule-builders.</param>
        IEnumerable<IEqualityRule> GetRules(IEnumerable<IBuildsRule> allBuilders = null);
    }
}
