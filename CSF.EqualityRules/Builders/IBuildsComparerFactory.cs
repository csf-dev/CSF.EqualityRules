using System;
using System.Collections.Generic;

namespace CSF.EqualityRules.Builders
{
    public interface IBuildsComparerFactory<out TEquality>
    {
        void UsingComparer<TComparer>() where TComparer : class,IEqualityComparer<TEquality>;
        void UsingComparer(Type comparerType);
        void UsingComparer(IEqualityComparer<TEquality> comparer);
        void Ignore();
    }
}