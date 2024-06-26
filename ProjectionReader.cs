using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryProviderExample
{

    public class ProjectionReader<T>: IEnumerable<T>, IEnumerable 
    {
        Enumerator<T>? enumerator;
        public ProjectionReader( DbDataReader reader, Delegate projector)
        {
            enumerator = new Enumerator<T>(reader, (Func<ProjectionRow,T>)projector);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(enumerator == null)
            {
                throw new InvalidOperationException("Cannot enumerate more than once");
            }
            var e = enumerator;
            enumerator = null;
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    internal class Enumerator<T> : ProjectionRow, IEnumerator<T>, IEnumerator, IDisposable
    {
        DbDataReader reader;
        T? current;
        Func<ProjectionRow, T> projector;

        public Enumerator(DbDataReader reader, Func<ProjectionRow,T> projector)
        {
            this.projector = projector;
            this.reader = reader;
        }
        public T Current => current!;

        object IEnumerator.Current => current!;

        public void Dispose()
        {
            reader.DisposeAsync();
        }

        public override object? GetValue(int index)
        {
            if(index >= 0)
            {
                if(reader.IsDBNull(index))
                {
                    return null;
                }
                return reader.GetValue(index);
            }
            throw new IndexOutOfRangeException();
        }

        public bool MoveNext()
        {
            if(reader.Read())
            {
                current = projector(this);
                return true;
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

}
