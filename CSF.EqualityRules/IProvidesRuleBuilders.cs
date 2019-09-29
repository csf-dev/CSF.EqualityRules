using System.Collections.Generic;
using CSF.EqualityRules.Builders;

namespace CSF.EqualityRules
{
    /// <summary>
    /// An object which can provide a collection of <see cref="RuleBuilder{T}"/>.
    /// </summary>
    public interface IProvidesRuleBuilders<T>
    {
        /// <summary>
        /// Gets the rule builders.
        /// </summary>
        /// <value>The rule builders.</value>
        ISet<RuleBuilder<T>> RuleBuilders { get; }
    }
}