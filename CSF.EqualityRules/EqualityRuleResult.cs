using System;

namespace CSF.EqualityRules
{
    public class EqualityRuleResult
    {
        public bool Passed { get; }
        public string Name { get; }
        public Exception Exception { get; }
        public object ValueA { get; }
        public object ValueB { get; }

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