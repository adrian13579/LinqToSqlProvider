using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryProviderExample
{
    public class DbQueryProvider(DbConnection connection) : MyQueryProvider
    {
        public override object Execute(Expression expression)
        {
            DbCommand command = connection.CreateCommand();
            var result = Translate(expression);
            command.CommandText = result.CommandText;

            DbDataReader reader = command.ExecuteReader();
            var elementType = GetEntityType(expression);

            var projector = result.Projector?.Compile();
            return Activator.CreateInstance(
                typeof(ProjectionReader<>).MakeGenericType(elementType),
                BindingFlags.Default,
                null,
                [reader, projector],
                null)!;
        }

        public override string GetQueryText(Expression expression)
        {
            return Translate(expression).CommandText;
        }

        private TranslateResult Translate(Expression expression)
        {
            throw new NotImplementedException();
        }

        private Type GetEntityType(Expression expression)
        {
            if (expression is MethodCallExpression m)
            {
                return m.Method.ReturnType.GetGenericArguments().First();
            }
            throw new NotSupportedException($"The expression of type '{expression.NodeType}' is not supported");
        }
    }
}
