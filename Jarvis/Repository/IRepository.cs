using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetByID(string id);
        string[] GetConnectionIDs(string id);
        T Add(string id, string connectionID);
        void RemoveConnection(string connectionID);
        void Remove(string id);
    }
}
