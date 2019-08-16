using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Rules
{
    public class ParentEqualityRule<TParent,TValue> : IGetsEqualityResult<TParent>
    {
        readonly IGetsValueFromParent<TParent,TValue> valueProvider;
        readonly IEqualityRule<TValue> valueRule;

        public string Name { get; set; }
        public event EventHandler<RuleCompletedEventArgs> RuleCompleted;
        public event EventHandler<RuleErroredEventArgs> RuleErrored;

        public bool Equals(TParent x, TParent y)
        {
            try
            {
                var valueX = valueProvider.GetValue(x);
                var valueY = valueProvider.GetValue(y);

                return valueRule.Equals(valueX, valueY);
            }
            catch (Exception e)
            {
                OnRuleErrored(e);
                return false;
            }
        }

        public int GetHashCode(TParent obj)
        {
            var value = valueProvider.GetValue(obj);
            return valueRule.GetHashCode(value);
        }

        public EqualityResult GetEqualityResult(TParent obj)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnRuleCompleted(object sender, RuleCompletedEventArgs args)
        {
            args.RuleName = Name;
            RuleCompleted?.Invoke(sender, args);
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

        protected virtual void OnRuleErrored(object sender, RuleErroredEventArgs args)
        {
            args.RuleName = Name;
            RuleErrored?.Invoke(sender, args);
        }

        public ParentEqualityRule(IGetsValueFromParent<TParent,TValue> valueProvider,
                                  IEqualityComparer<TValue> valueComparer)
        {
            if (valueComparer == null) throw new ArgumentNullException(nameof(valueComparer));

            this.valueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
            valueRule = new EqualityRule<TValue>(valueComparer);

            valueRule.RuleCompleted += OnRuleCompleted;
            valueRule.RuleErrored += OnRuleErrored;
        }
    }
}