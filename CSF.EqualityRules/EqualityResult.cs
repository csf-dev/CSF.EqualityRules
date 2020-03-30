using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.EqualityRules
{
    /// <summary>
    /// Provides information about the result of an equality comparison.
    /// </summary>
    public class EqualityResult
    {
        /// <summary>
        /// Gets a value indicating whether the two tested objects are equal or not.
        /// </summary>
        /// <value><c>true</c> if they are equal; otherwise, <c>false</c>.</value>
        public bool AreEqual { get; }

        /// <summary>
        /// Gets the results of individual equality rules.
        /// </summary>
        /// <value>The rule results.</value>
        public IReadOnlyCollection<EqualityRuleResult> RuleResults { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityResult"/> class.
        /// </summary>
        /// <param name="areEqual">If set to <c>true</c> then the tested objects are equal.</param>
        /// <param name="ruleResults">The rule results.</param>
        public EqualityResult(bool areEqual, IReadOnlyCollection<EqualityRuleResult> ruleResults)
        {
            AreEqual = areEqual;
            RuleResults = ruleResults ?? throw new ArgumentNullException(nameof(ruleResults));
        }

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="EqualityResult"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> that represents the current <see cref="EqualityResult"/>.</returns>
        public override string ToString()
        {
            if (AreEqual) return $"[{nameof(EqualityResult)}: Compared objects are equal]";

            var failedRuleNames = RuleResults.Where(x => !x.Passed).Select(x => x.Name).ToList();
            return $"[{nameof(EqualityResult)}: Compared objects are not equal.  Failed rules: {String.Join(", ", failedRuleNames)}]";
        }

        static EqualityResult()
        {
            Equal = new EqualityResult(true, Enumerable.Empty<EqualityRuleResult>().ToArray());
        }

        /// <summary>
        /// Gets a singleton instance indicating equality.
        /// </summary>
        /// <value>The result instance.</value>
        public static EqualityResult Equal { get; }
    }
}