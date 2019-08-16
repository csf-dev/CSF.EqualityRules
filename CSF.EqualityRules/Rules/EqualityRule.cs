using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Rules
{
    public class EqualityRule<T> : IEqualityRule<T>
    {
        readonly IEqualityComparer<T> comparer;

        public string Name { get; set; }
        public event EventHandler<RuleCompletedEventArgs> RuleCompleted;
        public event EventHandler<RuleErroredEventArgs> RuleErrored;

        public bool Equals(T x, T y)
        {
            try
            {
                var result = comparer.Equals(x, y);
                OnRuleCompleted(result, x, y);
                return result;
            }
            catch (Exception e)
            {
                OnRuleErrored(e);
                return false;
            }
        }

        public int GetHashCode(T obj) => comparer.GetHashCode(obj);


        protected virtual void OnRuleCompleted(bool areEqual, object x, object y)
        {
            var args = new RuleCompletedEventArgs
            {
                RuleName = Name,
                AreEqual = areEqual,
                ValueA = x,
                ValueB = y
            };
            RuleCompleted?.Invoke(this, args);
        }

        protected virtual void OnRuleErrored(Exception ex)
        {
            var args =  new RuleErroredEventArgs
            {
                RuleName = Name,
                Exception = ex
            };
            RuleErrored?.Invoke(this, args);
        }

        public EqualityRule(IEqualityComparer<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }
    }
}