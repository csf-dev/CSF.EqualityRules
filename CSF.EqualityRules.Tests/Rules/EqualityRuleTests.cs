using System;
using System.Collections.Generic;
using AutoFixture.NUnit3;
using CSF.EqualityRules.Rules;
using Moq;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests.Rules
{
    [TestFixture]
    public class EqualityRuleTests
    {
        [Test, AutoMoqData]
        public void Equals_returns_result_from_comparer([Frozen] IEqualityComparer<string> comparer,
                                                        EqualityRule<string> sut,
                                                        string a,
                                                        string b)
        {
            Mock.Get(comparer).Setup(x => x.Equals(a, b)).Returns(true);

            var result = sut.Equals(a, b);

            Assert.That(result, Is.True);
        }

        [Test, AutoMoqData]
        public void Equals_invokes_completion_event_if_equality_test_succeeds([Frozen] IEqualityComparer<string> comparer,
                                                                              EqualityRule<string> sut,
                                                                              string a,
                                                                              string b,
                                                                              string name)
        {
            RuleCompletedEventArgs args = null;
            Mock.Get(comparer).Setup(x => x.Equals(a, b)).Returns(true);
            EventHandler<RuleCompletedEventArgs> handler = (sender, eventArgs) => args = eventArgs;
            sut.Name = name;

            sut.RuleCompleted += handler;
            sut.Equals(a, b);
            sut.RuleCompleted -= handler;

            Assert.That(args?.ValueA, Is.EqualTo(a), nameof(RuleCompletedEventArgs.ValueA));
            Assert.That(args?.ValueB, Is.EqualTo(b), nameof(RuleCompletedEventArgs.ValueB));
            Assert.That(args?.AreEqual, Is.True, nameof(RuleCompletedEventArgs.AreEqual));
            Assert.That(args?.RuleName, Is.EqualTo(name), nameof(RuleCompletedEventArgs.RuleName));
        }

        [Test, AutoMoqData]
        public void Equals_invokes_completion_event_if_equality_test_fails_without_exception([Frozen] IEqualityComparer<string> comparer,
                                                                                             EqualityRule<string> sut,
                                                                                             string a,
                                                                                             string b,
                                                                                             string name)
        {
            RuleCompletedEventArgs args = null;
            Mock.Get(comparer).Setup(x => x.Equals(a, b)).Returns(false);
            EventHandler<RuleCompletedEventArgs> handler = (sender, eventArgs) => args = eventArgs;
            sut.Name = name;

            sut.RuleCompleted += handler;
            sut.Equals(a, b);
            sut.RuleCompleted -= handler;

            Assert.That(args?.ValueA, Is.EqualTo(a), nameof(RuleCompletedEventArgs.ValueA));
            Assert.That(args?.ValueB, Is.EqualTo(b), nameof(RuleCompletedEventArgs.ValueB));
            Assert.That(args?.AreEqual, Is.False, nameof(RuleCompletedEventArgs.AreEqual));
            Assert.That(args?.RuleName, Is.EqualTo(name), nameof(RuleCompletedEventArgs.RuleName));
        }

        [Test, AutoMoqData]
        public void Equals_invokes_error_event_if_comparison_throws([Frozen] IEqualityComparer<string> comparer,
                                                                    EqualityRule<string> sut,
                                                                    string a,
                                                                    string b,
                                                                    string name)
        {
            RuleErroredEventArgs args = null;
            Mock.Get(comparer).Setup(x => x.Equals(a, b)).Throws<InvalidOperationException>();
            EventHandler<RuleErroredEventArgs> handler = (sender, eventArgs) => args = eventArgs;
            sut.Name = name;

            sut.RuleErrored += handler;
            sut.Equals(a, b);
            sut.RuleErrored -= handler;

            Assert.That(args?.RuleName, Is.EqualTo(name), nameof(RuleErroredEventArgs.RuleName));
            Assert.That(args?.Exception, Is.InstanceOf<InvalidOperationException>(), nameof(RuleErroredEventArgs.Exception));
        }

        [Test, AutoMoqData]
        public void Equals_returns_false_if_comparison_throws([Frozen] IEqualityComparer<string> comparer,
                                                              EqualityRule<string> sut,
                                                              string a,
                                                              string b)
        {
            Mock.Get(comparer).Setup(x => x.Equals(a, b)).Throws<InvalidOperationException>();

            var result = sut.Equals(a, b);

            Assert.That(result, Is.False);
        }

        [Test, AutoMoqData]
        public void GetHashCode_returns_result_from_comparer([Frozen] IEqualityComparer<string> comparer,
                                                             EqualityRule<string> sut,
                                                             string str,
                                                             int hashCode)
        {
            Mock.Get(comparer).Setup(x => x.GetHashCode(str)).Returns(hashCode);

            var result = sut.GetHashCode(str);

            Assert.That(result, Is.EqualTo(hashCode));
        }
    }
}