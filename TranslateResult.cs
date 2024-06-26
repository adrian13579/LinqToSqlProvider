using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryProviderExample
{
    internal class TranslateResult
    {
        public required string CommandText;
        public LambdaExpression? Projector;
    }
}
