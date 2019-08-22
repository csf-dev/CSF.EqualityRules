using CSF.EqualityRules.ValueProviders;
using CSF.Reflection;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests.ValueProviders
{
    [TestFixture]
    public class ReflectionFieldValueProviderTests
    {
        [Test, AutoMoqData]
        public void GetValue_gets_field_value(ClassWithAField testClass, int fieldValue)
        {
            var field = Reflect.Field<ClassWithAField>(x => x.MyField);
            var sut = new ReflectionFieldValueProvider<ClassWithAField,int>(field);
            testClass.MyField = fieldValue;

            var result = sut.GetValue(testClass);

            Assert.That(result, Is.EqualTo(fieldValue));
        }

        #region contained class

        public class ClassWithAField
        {
            public int MyField;
        }

        #endregion
    }
}