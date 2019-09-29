using System;

namespace CSF.EqualityRules
{
    /// <summary>
    /// Gets information about a single equality rule.
    /// </summary>
    public class EqualityRuleResult
    {
        /// <summary>
        /// Gets a value indicating whether the rule passed or not.
        /// </summary>
        /// <value><c>true</c> if the rule passed; otherwise, <c>false</c>.</value>
        public bool Passed { get; }

        /// <summary>
        /// Gets the rule name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets an exception which occurred whilst executing the rule.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; }

        /// <summary>
        /// Gets the first value which was tested for equality (with <see cref="ValueB"/>).
        /// </summary>
        /// <value>The first value.</value>
        public object ValueA { get; }

        /// <summary>
        /// Gets the second value which was tested for equality (with <see cref="ValueA"/>).
        /// </summary>
        /// <value>The second value.</value>
        public object ValueB { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityRuleResult"/> class.
        /// </summary>
        /// <param name="name">Rule name.</param>
        /// <param name="passed">Whether the rule passed or not.</param>
        /// <param name="valueA">The first value.</param>
        /// <param name="valueB">The second value.</param>
        /// <param name="exception">The exception.</param>
        public EqualityRuleResult(string name, bool passed, object valueA = null, object valueB = null, Exception exception = null)
        {
            Name = name;
            Passed = passed;
            ValueA = valueA;
            ValueB = valueB;
            Exception = exception;
        }
    }
}