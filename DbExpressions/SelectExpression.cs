using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryProviderExample.DbExpressions
{
    internal class SelectExpression: SqlExpression
    {
        internal override SqlExpressionType NodeType
        {
            get { return SqlExpressionType.Select; }
        }
    }
}
