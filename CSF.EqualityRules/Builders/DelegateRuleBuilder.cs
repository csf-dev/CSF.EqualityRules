﻿using System;
using System.Collections.Generic;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;

namespace CSF.EqualityRules.Builders
{
    public interface IBuildsDelegateRule<TParent> : IBuildsRule
    {
        Func<TParent,object> Delegate { get; }
    }

    public class DelegateRuleBuilder<TParent,TValue> : RuleBuilder<TParent>, IBuildsDelegateRule<TParent>
    {
        public Func<TParent, TValue> Delegate { get; }
        public IEqualityComparer<TValue> Comparer { get; }

        Func<TParent, object> IBuildsDelegateRule<TParent>.Delegate => p => Delegate(p);

        public override IEnumerable<IEqualityRule<TParent>> GetRules()
        {
            var delegateRule = new EqualityRule<TValue>(Comparer, Name);
            var valueProvider = new DelegateValueProvider<TParent, TValue>(Delegate);
            var parentRule = new ParentEqualityRule<TParent, TValue>(valueProvider, delegateRule);
            return new[] { parentRule };
        }

        public DelegateRuleBuilder(Func<TParent, TValue> valueDelegate, IEqualityComparer<TValue> comparer = null)
        {
            Delegate = valueDelegate ?? throw new ArgumentNullException(nameof(valueDelegate));
            Comparer = comparer ?? EqualityComparer<TValue>.Default;
        }
    }
}
