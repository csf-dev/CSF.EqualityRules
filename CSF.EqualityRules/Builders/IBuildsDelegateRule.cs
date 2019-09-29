using System;

namespace CSF.EqualityRules.Builders
{
    /// <summary>
    /// A <see cref="IBuildsRule"/> which gets a value using a delegate.
    /// </summary>
    public interface IBuildsDelegateRule<TParent> : IBuildsRule
    {
        /// <summary>
        /// Gets the delegate used to retrieve the value for comparison.
        /// </summary>
        /// <value>The delegate.</value>
        Func<TParent, object> Delegate { get; }
    }
}
