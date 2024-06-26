using System.Collections;
using System.Linq.Expressions;

namespace QueryProviderExample
{
    public class MyQueryable<T> : IQueryable<T>, IQueryable, IOrderedQueryable<T>, IOrderedQueryable
    {
        private readonly MyQueryProvider _provider;
        private readonly Expression _expression;

        public MyQueryable(MyQueryProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);
            _provider = provider;
            _expression = Expression.Constant(this);
        }

        public MyQueryable(MyQueryProvider provider, Expression expression)
        {
            ArgumentNullException.ThrowIfNull(provider);
            ArgumentNullException.ThrowIfNull(expression);
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException(nameof(expression));
            }

            _provider = provider;
            _expression = expression;
        }
        public Type ElementType => typeof(T);

        public Expression Expression => _expression;

        public IQueryProvider Provider => _provider;

        public IEnumerator<T> GetEnumerator()
        {
            IQueryProvider provider = _provider;
            return provider.Execute<IEnumerable<T>>(_expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IQueryProvider provider = _provider;
            return provider.Execute<IEnumerable>(_expression).GetEnumerator();
        }

        public override string ToString() => _provider.GetQueryText(_expression);
    }
}
