using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Repository.Hack
{
    public class MemoryRepositoryFactory : IRepositoryFactory
    {

        private class RepoInfo
        {
            public Func<object> Create { get; set; }
            public object Repository { get; set; }
        }

        private Dictionary<Type, RepoInfo> __registry; 

        private MemoryRepositoryFactory()
        {
            __registry = new Dictionary<Type, RepoInfo>();
            __registry.Add(typeof(Client), new RepoInfo()
            {
                Create = () => new ClientRepository()
            });
        }

        

        private object GetRepo(Type type)
        {
            RepoInfo info = __registry[type];
            if (info.Repository == null)
            {
                info.Repository = info.Create();
            }
            return info.Repository;
        }

        private static MemoryRepositoryFactory __instance;
        
        public static MemoryRepositoryFactory Instance
        {
            get
            {
                if (__instance == null)
                {
                    __instance = new MemoryRepositoryFactory();
                }
                return __instance;
            }
        }


        public IRepository<T> GetRepository<T>() where T : class
        {
            return  (IRepository<T>)GetRepo(typeof(T));
        }
    }
}