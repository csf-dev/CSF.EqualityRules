using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.NUnit3;
using CSF.EqualityRules.Rules;
using Moq;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests.Rules
{
    [TestFixture,Parallelizable]
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

        [Test, AutoMoqData]
        public void GetEqualityResult_returns_equals_result_when_instances_are_equal([Frozen] IEqualityComparer<string> comparer,
                                                                                     EqualityRule<string> sut,
                                                                                     string a,
                                                                                     string b)
        {
            Mock.Get(comparer).Setup(x => x.Equals(a, b)).Returns(true);

            var result = sut.GetEqualityResult(a, b);

            Assert.That(result?.AreEqual, Is.True);
        }

        [Test, AutoMoqData]
        public void GetEqualityResult_returns_not_equal_result_when_instances_are_equal([Frozen] IEqualityComparer<string> comparer,
                                                                                        EqualityRule<string> sut,
                                                                                        string a,
                                                                                        string b)
        {
            Mock.Get(comparer).Setup(x => x.Equals(a, b)).Returns(false);

            var result = sut.GetEqualityResult(a, b);

            Assert.That(result?.AreEqual, Is.False);
        }

        [Test, AutoMoqData]
        public void GetEqualityResult_includes_values_in_result([Frozen] IEqualityComparer<string> comparer,
                                                                EqualityRule<string> sut,
                                                                string a,
                                                                string b)
        {
            var result = sut.GetEqualityResult(a, b);

            Assert.That(result?.RuleResults?.FirstOrDefault()?.ValueA, Is.EqualTo(a), "First value");
            Assert.That(result?.RuleResults?.FirstOrDefault()?.ValueB, Is.EqualTo(b), "Second value");
        }
    }
}