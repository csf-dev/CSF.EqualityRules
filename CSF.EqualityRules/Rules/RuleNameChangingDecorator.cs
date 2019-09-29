using System;
using System.Linq;

namespace CSF.EqualityRules.Rules
{
    /// <summary>
    /// Decorator for an <see cref="IEqualityRule{T}"/> which modifies/transforms the rule name.
    /// </summary>
    public class RuleNameChangingDecorator<T> : IEqualityRule<T>
    {
        readonly IEqualityRule<T> wrapped;
        readonly Func<string, string> nameTransformer;
        readonly EqualityResultFactory resultFactory;

        /// <summary>When overridden in a derived class, determines whether two objects of type <typeparamref name="T"/> name="T" /> are equal.</summary>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public bool Equals(T x, T y) => wrapped.Equals(x, y);

        /// <summary>When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.</summary>
        /// <returns>A hash code for the specified object.</returns>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
        public int GetHashCode(T obj) => wrapped.GetHashCode(obj);

        /// <summary>
        /// Gets a detailled equality comparison result.
        /// </summary>
        /// <returns>The equality result.</returns>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        public EqualityResult GetEqualityResult(T x, T y)
        {
            var wrappedResult = wrapped.GetEqualityResult(x, y);
            if (wrappedResult == null) return null;

            var results = wrappedResult.RuleResults
                .Select(res => new EqualityRuleResult(nameTransformer(res.Name), res.Passed, res.ValueA, res.ValueB, res.Exception))
                .ToArray();
            return resultFactory.GetEqualityResult(results);
        }

        /// <summary>
        /// Gets the rule name.
        /// </summary>
        /// <value>The rule name.</value>
        public string Name => nameTransformer(wrapped.Name);

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleNameChangingDecorator{T}"/> class.
        /// </summary>
        /// <param name="wrapped">The wrapped equality rule.</param>
        /// <param name="nameTransformer">A function which transforms the rule name.</param>
        public RuleNameChangingDecorator(IEqualityRule<T> wrapped, Func<string,string> nameTransformer)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
            this.nameTransformer = nameTransformer ?? throw new ArgumentNullException(nameof(nameTransformer));
            resultFactory = new EqualityResultFactory();
        }
    }
}