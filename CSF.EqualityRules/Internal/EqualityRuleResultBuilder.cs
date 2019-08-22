using System;

namespace CSF.EqualityRules.Internal
{
    public class EqualityRuleResultBuilder
    {
        Exception exception;
        object valueA, valueB;
        string name;

        public EqualityRuleResult GetRuleResult<T>(IEqualityRule<T> rule, T x, T y)
        {
            bool result;

            rule.RuleCompleted += OnRuleCompleted;
            rule.RuleErrored += OnRuleErrored;

            try
            {
                result = rule.Equals(x, y);
            }
            finally
            {
                rule.RuleCompleted -= OnRuleCompleted;
                rule.RuleErrored -= OnRuleErrored;
            }

            return new EqualityRuleResult(name, result, valueA, valueB, exception);
        }

        void OnRuleErrored(object sender, RuleErroredEventArgs e)
        {
            name = e.RuleName;
            exception = e.Exception;
        }

        void OnRuleCompleted(object sender, RuleCompletedEventArgs e)
        {
            name = e.RuleName;
            valueA = e.ValueA;
            valueB = e.ValueB;
        }
    }
}