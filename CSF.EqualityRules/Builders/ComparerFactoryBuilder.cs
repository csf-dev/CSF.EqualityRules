using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Builders
{
    public class ComparerFactoryBuilder<T> : IBuildsComparerFactory<T>
    {
        readonly IResolvesServices resolver;

        public Func<IEqualityComparer<T>> Comparer { get; private set; }

        public void UsingComparer<TComparer>() where TComparer : class,IEqualityComparer<T>
        {
            Comparer = () => resolver.Resolve<TComparer>();
        }

        public void UsingComparer(Type comparerType)
        {
            Comparer = () => (IEqualityComparer<T>) resolver.Resolve(comparerType);
        }

        public void UsingComparer(IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            Comparer = () => comparer;
        }

        public void Ignore()
        {
            Comparer = () => new AlwaysEqualComparer<T>();
        }

        public ComparerFactoryBuilder(IResolvesServices resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            Comparer = null;
        }
    }
}