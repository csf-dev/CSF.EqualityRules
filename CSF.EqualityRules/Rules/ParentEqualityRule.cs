using System;
using System.Collections.Generic;
using CSF.EqualityRules.Internal;

namespace CSF.EqualityRules.Rules
{
    public class ParentEqualityRule<TParent,TValue> : IGetsEqualityResult<TParent>, IEqualityRule<TParent>
    {
        readonly IGetsValueFromParent<TParent,TValue> valueProvider;
        readonly IGetsEqualityResultFromRuleResults resultFactory;
        readonly IGetsEqualityRuleResult ruleResultFactory;
        readonly EqualityRule<TValue> valueRule;

        public string Name
        {
            get { return valueRule.Name; }
            set { valueRule.Name = value; }
        }

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

        public EqualityResult GetEqualityResult(TParent x, TParent y)
        {
            var ruleResult = ruleResultFactory.GetResult(this, x, y);
            return resultFactory.GetEqualityResult(new[] {ruleResult});
        }

        protected virtual void OnRuleCompleted(object sender, RuleCompletedEventArgs args)
        {
            args.RuleName = Name;
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

        protected virtual void OnRuleErrored(object sender, RuleErroredEventArgs args)
        {
            args.RuleName = Name;
            RuleErrored?.Invoke(this, args);
        }

        public ParentEqualityRule(IGetsValueFromParent<TParent,TValue> valueProvider,
                                  IEqualityComparer<TValue> valueComparer,
                                  IGetsEqualityRuleResult ruleResultFactory = null,
                                  IGetsEqualityResultFromRuleResults resultFactory = null)
        {
            if (valueComparer == null) throw new ArgumentNullException(nameof(valueComparer));

            this.valueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
            this.resultFactory = resultFactory ?? new EqualityResultFactory();
            this.ruleResultFactory = ruleResultFactory ?? new EqualityRuleResultFactory();
            valueRule = new EqualityRule<TValue>(valueComparer);

            valueRule.RuleCompleted += OnRuleCompleted;
            valueRule.RuleErrored += OnRuleErrored;
        }
    }
}