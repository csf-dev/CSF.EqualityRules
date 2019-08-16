using System;

namespace CSF.EqualityRules
{
    public class RuleCompletedEventArgs : EventArgs
    {
        public string RuleName { get; set; }
        public bool AreEqual { get; set; }
        public object ValueA { get; set; }
        public object ValueB { get; set; }
    }
}