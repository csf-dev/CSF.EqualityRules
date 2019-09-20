using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Rules
{
    public class EqualityRule<T> : IEqualityRule<T>
    {
        readonly IEqualityComparer<T> comparer;
        readonly EqualityResultFactory resultFactory;

        public string Name { get; }

        public bool Equals(T x, T y) => GetEqualityResult(x, y).AreEqual;

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

        public EqualityRule(IEqualityComparer<T> comparer, string name)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            Name = name;

            resultFactory = new EqualityResultFactory();
        }
    }
}