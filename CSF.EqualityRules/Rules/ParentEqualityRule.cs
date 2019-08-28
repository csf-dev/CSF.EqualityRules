using System;

namespace CSF.EqualityRules.Rules
{
    public class ParentEqualityRule<TParent,TValue> : IEqualityRule<TParent>
    {
        readonly IGetsValueFromParent<TParent,TValue> valueProvider;
        readonly IEqualityRule<TValue> valueRule;
        readonly EqualityResultFactory resultFactory;

        public string Name { get; set; }

        public bool Equals(TParent x, TParent y) => GetEqualityResult(x, y).AreEqual;

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

        public ParentEqualityRule(IGetsValueFromParent<TParent,TValue> valueProvider,
                                  IEqualityRule<TValue> valueRule)
        {
            this.valueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
            this.valueRule = valueRule ?? throw new ArgumentNullException(nameof(valueRule));

            resultFactory = new EqualityResultFactory();
        }
    }
}