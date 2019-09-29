using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Rules
{
    /// <summary>
    /// Default implementation of an equality rule.
    /// </summary>
    public class EqualityRule<T> : IEqualityRule<T>
    {
        readonly IEqualityComparer<T> comparer;
        readonly EqualityResultFactory resultFactory;

        /// <summary>
        /// Gets the rule name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <exception cref="T:System.ArgumentException" />
        /// <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other.
        public bool Equals(T x, T y) => GetEqualityResult(x, y).AreEqual;

        /// <summary>
        /// Determines whether the specified objects are equal, returning information which indicates the results
        /// of equality rules which have passed/failed.
        /// </summary>
        /// <returns>An equality result.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public EqualityResult GetEqualityResult(T x, T y)
        {
            var ruleResult = GetEqualityRuleResult(x, y);
            return resultFactory.GetEqualityResult(new[] { ruleResult });
        }

        EqualityRuleResult GetEqualityRuleResult(T x, T y)
        {
            try
            {
                var result = comparer.Equals(x, y);
                return new EqualityRuleResult(Name, result, x, y);
            }
            catch (Exception e)
            {
                return new EqualityRuleResult(Name, false, x, y, e);
            }
        }

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <returns>A hash code for the specified object.</returns>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
        public int GetHashCode(T obj)
        {
            try
            {
                return comparer.GetHashCode(obj);
            }
            catch(InvalidOperationException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new InvalidOperationException($"{nameof(GetHashCode)} raised an unexpected exception.", e);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityRule{T}"/> class.
        /// </summary>
        /// <param name="comparer">An equality comparer.</param>
        /// <param name="name">The rule name.</param>
        public EqualityRule(IEqualityComparer<T> comparer, string name)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            Name = name;

            resultFactory = new EqualityResultFactory();
        }
    }
}