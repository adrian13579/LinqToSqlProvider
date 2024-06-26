using System.Linq.Expressions;

namespace QueryProviderExample
{
    public abstract class MyQueryProvider : System.Linq.IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            var elementType = expression.Type.GetGenericArguments()[0];
            return (IQueryable)Activator.CreateInstance(typeof(MyQueryable<>).MakeGenericType(elementType), [this, expression])!;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MyQueryable<TElement>(this, expression);
        }


        object? System.Linq.IQueryProvider.Execute(Expression expression)
        {
            return Execute(expression)!;
        }

        TResult System.Linq.IQueryProvider.Execute<TResult>(Expression expression)
        {
            return (TResult)Execute(expression)!;
        }
        public abstract object Execute(Expression expression);
        public abstract string GetQueryText(Expression expression);
    }
}
