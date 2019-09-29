using CSF.EqualityRules.ValueProviders;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests.ValueProviders
{
    [TestFixture,Parallelizable]
    public class DelegateValueProviderTests
    {
        [Test, AutoMoqData]
        public void GetValue_executes_delegate(string str, EmptyClass obj)
        {
            var sut = new DelegateValueProvider<EmptyClass, string>(x => x == obj? str : null);
            var result = sut.GetValue(obj);
            Assert.That(result, Is.EqualTo(str));
        }

        #region contained class

        public class EmptyClass {}

        #endregion
    }
}