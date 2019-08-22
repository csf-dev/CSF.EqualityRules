using CSF.EqualityRules.ValueProviders;
using CSF.Reflection;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests.ValueProviders
{
    [TestFixture]
    public class ReflectionPropertyValueProviderTests
    {
        [Test, AutoMoqData]
        public void GetValue_gets_property_value(ClassWithAProperty testClass, int propValue)
        {
            var property = Reflect.Property<ClassWithAProperty>(x => x.MyProperty);
            var sut = new ReflectionPropertyValueProvider<ClassWithAProperty,int>(property);
            testClass.MyProperty = propValue;

            var result = sut.GetValue(testClass);

            Assert.That(result, Is.EqualTo(propValue));
        }

        #region contained class

        public class ClassWithAProperty
        {
            public int MyProperty { get; set; }
        }

        #endregion
    }
}