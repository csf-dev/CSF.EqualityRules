using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSF.EqualityRules
{
    public static class EqualityBuilderExtensions
    {
        public static EqualityBuilder<TParent> ForProperty<TParent,TProp>(this EqualityBuilder<TParent> builder,
                                                                          Expression<Func<TParent,TProp>> prop,
                                                                          IEqualityComparer<TProp> comparer = null,
                                                                          string name = null)
        {

        }
    }
}
