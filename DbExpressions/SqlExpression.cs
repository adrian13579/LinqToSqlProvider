﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryProviderExample.DbExpressions
{
    internal abstract class SqlExpression
    {
        internal abstract SqlExpressionType NodeType { get; }

    }
}
