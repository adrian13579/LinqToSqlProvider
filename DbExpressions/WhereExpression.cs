using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryProviderExample.DbExpressions
{
    internal class WhereExpression : SqlExpression
    {
        internal override SqlExpressionType NodeType => SqlExpressionType.Where;
    }
}
