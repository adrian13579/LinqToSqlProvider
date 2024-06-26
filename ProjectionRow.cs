using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryProviderExample
{
    public abstract class ProjectionRow
    {
        public abstract object? GetValue(int index);
    }
}
