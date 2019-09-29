using System;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Rules
{
    /// <summary>
    /// An equality rule which tests equality by traversing from a parent object reference to a 'child' value,
    /// such as via a property/field access.
    /// </summary>
    public class ParentEqualityRule<TParent,TValue> : IEqualityRule<TParent>
    {
        readonly IGetsValueFromParent<TParent,TValue> valueProvider;
        readonly IEqualityRule<TValue> valueRule;
        readonly EqualityResultFactory resultFactory;

        /// <summary>
        /// Gets the rule name.
        /// </summary>
        /// <value>The rule name.</value>
        public string Name => valueRule.Name;

        /// <summary>When overridden in a derived class, determines whether two objects of type <typeparamref name="TParent"/> name="T" /> are equal.</summary>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public bool Equals(TParent x, TParent y) => GetEqualityResult(x, y).AreEqual;

        /// <summary>
        /// Gets a detailled equality comparison result.
        /// </summary>
        /// <returns>The equality result.</returns>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        public EqualityResult GetEqualityResult(TParent x, TParent y)
        {
            try
            {
                var valueX = valueProvider.GetValue(x);
                var valueY = valueProvider.GetValue(y);

                return valueRule.GetEqualityResult(valueX, valueY);
            }
            catch (Exception e)
            {
                var ruleResult = new EqualityRuleResult(Name, false, exception: e);
                return resultFactory.GetEqualityResult(new[] { ruleResult });
            }
        }

        /// <summary>When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.</summary>
        /// <returns>A hash code for the specified object.</returns>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
        public int GetHashCode(TParent obj)
        {
            try
            {
                var val = valueProvider.GetValue(obj);
                return valueRule.GetHashCode(val);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"{nameof(GetHashCode)} raised an unexpected exception.", e);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParentEqualityRule{TParent, TValue}"/> class.
        /// </summary>
        /// <param name="valueProvider">A service which gets the value to be compared from the parent object.</param>
        /// <param name="valueRule">A equality rule which tests the 'child' value.</param>
        public ParentEqualityRule(IGetsValueFromParent<TParent,TValue> valueProvider,
                                  IEqualityRule<TValue> valueRule)
        {
            this.valueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
            this.valueRule = valueRule ?? throw new ArgumentNullException(nameof(valueRule));

            resultFactory = new EqualityResultFactory();
        }
    }
}