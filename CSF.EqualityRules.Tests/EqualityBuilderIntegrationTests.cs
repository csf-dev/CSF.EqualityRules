using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSF.EqualityRules.Tests
{
    [TestFixture,Parallelizable]
    public class EqualityBuilderIntegrationTests
    {
        [Test,AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichTestsUsingAComparer_AndDetectsEqualObjectsAccordingly(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.StringProp, c => c.UsingComparer(StringComparer.InvariantCultureIgnoreCase))
                .Build();

            one.StringProp = "foo";
            two.StringProp = "FOO";

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.True);
        }

        [Test, AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichTestsUsingAComparer_AndDetectsInequalObjectsAccordingly(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.StringProp, c => c.UsingComparer(StringComparer.InvariantCulture))
                .Build();

            one.StringProp = "foo";
            two.StringProp = "FOO";

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.False);
        }

        [Test, AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichTestsUsingAGenericComparerType_AndDetectsEqualObjectsAccordingly(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.IntProp, c => c.UsingComparer<CloseEnoughComparer>())
                .Build();

            one.IntProp = 2;
            two.IntProp = 3;

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.True);
        }

        [Test, AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichTestsUsingAGenericComparerType_AndDetectsInequalObjectsAccordingly(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.IntProp, c => c.UsingComparer<CloseEnoughComparer>())
                .Build();

            one.IntProp = 2;
            two.IntProp = 30;

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.False);
        }

        [Test, AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichTestsUsingAComparerType_AndDetectsEqualObjectsAccordingly(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.IntProp, c => c.UsingComparer(typeof(CloseEnoughComparer)))
                .Build();

            one.IntProp = 2;
            two.IntProp = 3;

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.True);
        }

        [Test, AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichTestsUsingAComparerType_AndDetectsInequalObjectsAccordingly(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.IntProp, c => c.UsingComparer(typeof(CloseEnoughComparer)))
                .Build();

            one.IntProp = 2;
            two.IntProp = 30;

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.False);
        }

        [Test, AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichFailsIfOneRulePassesButAnotherFails(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.IntProp, c => c.UsingComparer(typeof(CloseEnoughComparer)))
                .ForProperty(x => x.StringProp)
                .Build();

            one.IntProp = 2;
            one.StringProp = "foo";
            two.IntProp = 3;
            two.StringProp = "bar";

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.False);
        }

        [Test, AutoMoqData]
        public void Build_ReturnsAnEqualityComparerWhichPassesIfAllRulesPass(SampleClass one, SampleClass two)
        {
            var comparer = new EqualityBuilder<SampleClass>()
                .ForProperty(x => x.IntProp, c => c.UsingComparer(typeof(CloseEnoughComparer)))
                .ForProperty(x => x.StringProp)
                .Build();

            one.IntProp = 2;
            one.StringProp = "foo";
            two.IntProp = 3;
            two.StringProp = "foo";

            var result = comparer.Equals(one, two);

            Assert.That(result, Is.True);
        }

        #region contained classes

        public class SampleClass
        {
            public string StringProp { get; set; }
            public int IntProp { get; set; }
        }

        public class CloseEnoughComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                if (Math.Abs(x - y) <= 5) return true;
                return false;
            }

            public int GetHashCode(int obj)
            {
                // Note: This is a terrible hash code implementation because equal objects
                // would not necessarily have the same hash code.
                return obj.GetHashCode();
            }
        }

        #endregion
    }
}
