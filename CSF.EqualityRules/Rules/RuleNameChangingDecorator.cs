using System;
using System.Linq;

namespace CSF.EqualityRules.Rules
{
    public class RuleNameChangingDecorator<T> : IEqualityRule<T>
    {
        readonly IEqualityRule<T> wrapped;
        readonly Func<string, string> nameTransformer;
        readonly EqualityResultFactory resultFactory;

        public bool Equals(T x, T y) => wrapped.Equals(x, y);
        public int GetHashCode(T obj) => wrapped.GetHashCode(obj);

        public EqualityResult GetEqualityResult(T x, T y)
        {
            var wrappedResult = wrapped.GetEqualityResult(x, y);
            if (wrappedResult == null) return null;

            var results = wrappedResult.RuleResults
                .Select(res => new EqualityRuleResult(nameTransformer(res.Name), res.Passed, res.ValueA, res.ValueB, res.Exception))
                .ToArray();
            return resultFactory.GetEqualityResult(results);
        }

        public string Name => nameTransformer(wrapped.Name);

        public RuleNameChangingDecorator(IEqualityRule<T> wrapped, Func<string,string> nameTransformer)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
            this.nameTransformer = nameTransformer ?? throw new ArgumentNullException(nameof(nameTransformer));
            resultFactory = new EqualityResultFactory();
        }
    }
}