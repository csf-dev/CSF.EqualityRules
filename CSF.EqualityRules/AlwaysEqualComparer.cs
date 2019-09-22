using System;
using System.Collections;
using System.Collections.Generic;

namespace CSF.EqualityRules
{
    public class AlwaysEqualComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y) => true;

        public int GetHashCode(object obj) => 0;
    }

    public class AlwaysEqualComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y) => true;

        public int GetHashCode(T obj) => 0;
    }
}
