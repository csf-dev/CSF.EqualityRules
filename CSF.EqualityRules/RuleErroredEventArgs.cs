using System;

namespace CSF.EqualityRules
{
    public class RuleErroredEventArgs : EventArgs
    {
        public string RuleName { get; set; }
        public Exception Exception { get; set; }
    }
}