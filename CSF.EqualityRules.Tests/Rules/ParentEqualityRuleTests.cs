using AutoFixture.NUnit3;
using CSF.EqualityRules.Rules;
using CSF.EqualityRules.ValueProviders;
using Moq;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests.Rules
{
    [TestFixture,Parallelizable]
    public class ParentEqualityRuleTests
    {
        [Test, AutoMoqData]
        public void Equals_uses_value_provider_to_get_values_and_passes_to_wrapped_rule([Frozen] IGetsValueFromParent<EmptyClass,string> valueProvider,
                                                                                        [Frozen] IEqualityRule<string> rule,
                                                                                        ParentEqualityRule<EmptyClass,string> sut,
                                                                                        EmptyClass obj1,
                                                                                        EmptyClass obj2,
                                                                                        string str1,
                                                                                        string str2)
        {
            Mock.Get(valueProvider).Setup(x => x.GetValue(obj1)).Returns(str1);
            Mock.Get(valueProvider).Setup(x => x.GetValue(obj2)).Returns(str2);
            Mock.Get(rule).Setup(x => x.GetEqualityResult(str1, str2)).Returns(EqualityResult.Equal);

            var result = sut.Equals(obj1, obj2);

            Mock.Get(rule).Verify(x => x.GetEqualityResult(str1, str2), Times.Once);
            Assert.That(result, Is.True);
        }

        [Test, AutoMoqData]
        public void GetHashCode_uses_value_provider_to_get_value_and_passes_to_wrapped_rule([Frozen] IGetsValueFromParent<EmptyClass,string> valueProvider,
                                                                                            [Frozen] IEqualityRule<string> rule,
                                                                                            ParentEqualityRule<EmptyClass,string> sut,
                                                                                            EmptyClass obj,
                                                                                            string str,
                                                                                            int hashCode)
        {
            Mock.Get(valueProvider).Setup(x => x.GetValue(obj)).Returns(str);
            Mock.Get(rule).Setup(x => x.GetHashCode(str)).Returns(hashCode);

            var result = sut.GetHashCode(obj);

            Mock.Get(rule).Verify(x => x.GetHashCode(str), Times.Once);
            Assert.That(result, Is.EqualTo(hashCode));
        }

        #region contained class

        public class EmptyClass {}

        #endregion
    }
}