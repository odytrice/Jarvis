using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Repository
{
    public interface IRepository<T> where T : class
    {
        T Find(string id);
        void Add(T instance);
        void Remove(string id);
        void Remove(T instance);
        IEnumerable<T> Fetch(System.Linq.Expressions.Expression<Func<T, bool>> filter);
    }
}
