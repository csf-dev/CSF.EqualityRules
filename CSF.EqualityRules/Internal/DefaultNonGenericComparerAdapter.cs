using System;
using System.Collections;
using System.Collections.Generic;

namespace CSF.EqualityRules.Internal
{
    public class DefaultNonGenericComparerAdapter : IEqualityComparer, IEqualityComparer<object>
    {
        static readonly IEqualityComparer<object> wrapped;

        public new bool Equals(object x, object y)
        {
            return wrapped.Equals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return wrapped.GetHashCode(obj);
        }

        static DefaultNonGenericComparerAdapter()
        {
            wrapped = EqualityComparer<object>.Default;
        }
    }
}
