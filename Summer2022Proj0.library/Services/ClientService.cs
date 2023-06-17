using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Summer2022Proj0.library.Models;

namespace Summer2022Proj0.library.Services
{
    public class ClientService
    {
        private static ClientService? instance;
        private static object _lock = new object();
        public static ClientService Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new ClientService();
                    }
                }
                return instance;
            }
        }
        private List<Client> clients;
        private ClientService()
        {
            //clients = new List<Client>();
            clients = new List<Client>
            {
                new Client{Notes = "test1", Name = "Jane Doe", Id = 1, OpenDate = DateTime.MinValue, ClosedDate = DateTime.MinValue, IsActive = false},
                new Client{Notes = "test2", Name = "Bob Smith", Id = 2, OpenDate = DateTime.Today, ClosedDate = DateTime.Today, IsActive = true},
                new Client{Notes = "test3", Name = "Suzy Johnson", Id = 3, OpenDate = DateTime.MaxValue, ClosedDate = DateTime.MaxValue, IsActive = true}
            };

        }
        public List<Client> Clients
        {
            get
            {
                return clients;
            }
        }
        public Client? Get(int id)//? means returns explicit null
        {
            return clients.FirstOrDefault(e => e.Id == id);
        }
        public void Add(Client? client)
        {
            bool clientExists = false;
            int validId = 0;
            for (int i = 1; i > 0; i++)
            {
                foreach(Client? each in clients)
                {
                    if (i == each.Id)
                    {
                        clientExists = true;
                        break;
                    }
                }
                if (clientExists == false)
                {
                    validId = i;
                    break;
                }
                clientExists = false;
            }
            if(validId != 0 && client != null)
                client.Id = validId;
            if (client != null)
            {
                clients.Add(client);
            }
        }
        public void Delete(int id)
        {
            var enrollmentToRemove = Get(id);
            if (enrollmentToRemove != null)
            {
                clients.Remove(enrollmentToRemove);
            }
        }
        public void Delete(Client s)
        {
            Delete(s.Id);
        }
        public List<Client> Search(string query)
        {
            return clients.Where(s => s.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }
    }
}
