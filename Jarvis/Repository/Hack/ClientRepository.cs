using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Repository.Hack
{
    public class ClientRepository : IRepository<Client>
    {
        private List<Client> clients;
        
        public ClientRepository()
        {
            clients = new List<Client>();
        }

        public Client GetByID(string id)
        {
            return clients.FirstOrDefault();
            //return clients.FirstOrDefault(x => x.ID == id);
        }

        public string[] GetConnectionIDs(string id)
        {
            return clients.Where(x => x.ID == id).Select(v => v.ConnectionID).ToArray();
        }

        public Client Add(string id, string connectionID)
        {
            Client c = new Client()
            {
                ID = id, ConnectionID = connectionID
            };
            clients.Add(c);
            return c;
        }

        public void RemoveConnection(string connectionID)
        {
            clients.RemoveAll(x => x.ConnectionID == connectionID);
        }

        public void Remove(string id)
        {
            clients.RemoveAll(x => x.ID == id);
        }
    }
}