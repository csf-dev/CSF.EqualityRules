using System.Linq;
using CSF.EqualityRules.Rules;
using Moq;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests.Rules
{
    [TestFixture,Parallelizable]
    public class MultipleEqualityRuleRunnerTests
    {
        [Test, AutoMoqData]
        public void GetEqualityResult_returns_rule_result_for_each_rule(IEqualityRule<string> rule1,
                                                                        IEqualityRule<string> rule2,
                                                                        string a,
                                                                        string b)
        {
            var sut = new MultipleEqualityRuleRunner<string>(new [] {rule1, rule2});
            Mock.Get(rule1)
                .Setup(x => x.GetEqualityResult(a, b))
                .Returns(new EqualityResult(true, new[] {new EqualityRuleResult("Rule 1", true),}));
            Mock.Get(rule2)
                .Setup(x => x.GetEqualityResult(a, b))
                .Returns(new EqualityResult(true, new[] {new EqualityRuleResult("Rule 2", true),}));

            var result = sut.GetEqualityResult(a, b);

            Assert.That(result?.RuleResults?.Select(x => x.Name), Is.EquivalentTo(new [] {"Rule 1", "Rule 2"}));
        }

        [Test, AutoMoqData]
        public void GetEqualityResult_returns_true_result_if_all_rules_pass(IEqualityRule<string> rule1,
                                                                            IEqualityRule<string> rule2,
                                                                            string a,
                                                                            string b)
        {
            var sut = new MultipleEqualityRuleRunner<string>(new [] {rule1, rule2});
            Mock.Get(rule1)
                .Setup(x => x.GetEqualityResult(a, b))
                .Returns(new EqualityResult(true, new[] {new EqualityRuleResult("Rule 1", true),}));
            Mock.Get(rule2)
                .Setup(x => x.GetEqualityResult(a, b))
                .Returns(new EqualityResult(true, new[] {new EqualityRuleResult("Rule 2", true),}));

            var result = sut.GetEqualityResult(a, b);

            Assert.That(result?.AreEqual, Is.True);
        }

        [Test, AutoMoqData]
        public void GetEqualityResult_returns_false_result_if_any_rule_fails(IEqualityRule<string> rule1,
                                                                             IEqualityRule<string> rule2,
                                                                             string a,
                                                                             string b)
        {
            var sut = new MultipleEqualityRuleRunner<string>(new [] {rule1, rule2});
            Mock.Get(rule1)
                .Setup(x => x.GetEqualityResult(a, b))
                .Returns(new EqualityResult(true, new[] {new EqualityRuleResult("Rule 1", true),}));
            Mock.Get(rule2)
                .Setup(x => x.GetEqualityResult(a, b))
                .Returns(new EqualityResult(true, new[] {new EqualityRuleResult("Rule 2", false),}));

            var result = sut.GetEqualityResult(a, b);

            Assert.That(result?.AreEqual, Is.False);
        }

        [Test, AutoMoqData]
        public void GetHashCode_gets_hash_code_from_every_rule(IEqualityRule<string> rule1,
                                                               IEqualityRule<string> rule2,
                                                               string str)
        {
            var sut = new MultipleEqualityRuleRunner<string>(new [] {rule1, rule2});

            sut.GetHashCode(str);

            Mock.Get(rule1).Verify(x => x.GetHashCode(str), Times.Once);
            Mock.Get(rule2).Verify(x => x.GetHashCode(str), Times.Once);
        }
    }
}