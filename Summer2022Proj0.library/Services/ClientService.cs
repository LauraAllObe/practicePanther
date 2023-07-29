using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.library.Models;
using Summer2022Proj0.Library.Utilities;

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
        private ClientService()
        {
            var response = new WebRequestHandler()
                    .Get("/Client")
                    .Result;
            clients = JsonConvert
                .DeserializeObject<List<ClientDTO>>(response)
                ?? new List<ClientDTO>();
            /*clients = new List<Client>
            {
                new Client{Notes = "test1", Name = "Jane Doe", Id = 1, OpenDate = DateTime.MinValue, ClosedDate = DateTime.MinValue, IsActive = false},
                new Client{Notes = "test2", Name = "Bob Smith", Id = 2, OpenDate = DateTime.Today, ClosedDate = DateTime.Today, IsActive = true},
                new Client{Notes = "test3", Name = "Suzy Johnson", Id = 3, OpenDate = DateTime.MaxValue, ClosedDate = DateTime.MaxValue, IsActive = true}
            };*/

        }
        private List<ClientDTO> clients;
        public List<ClientDTO> Clients
        {
            get
            {
                return clients ?? new List<ClientDTO>();
            }
        }
        public ClientDTO? Get(int id)
        {
            return Clients.FirstOrDefault(c => c.Id == id);
        }
        public void AddOrEdit(ClientDTO c)
        {
            var response
                = new WebRequestHandler().Post("/Client", c).Result;
            //MISSING CODE
            var myEditedClient = JsonConvert.DeserializeObject<ClientDTO>(response);
            if (myEditedClient != null)
            {
                var existingClient = clients.FirstOrDefault(c => c.Id == myEditedClient.Id);
                if (existingClient == null)
                {
                    clients.Add(myEditedClient);
                }
                else
                {
                    var index = clients.IndexOf(existingClient);
                    clients.Remove(existingClient);
                    clients.Insert(index, myEditedClient);
                }
            }
        }
        public void Delete(int id)
        {
            var clientToDelete = Clients.FirstOrDefault(c => c.Id == id);
            if (clientToDelete != null)
            {
                var response
                = new WebRequestHandler().Delete($"/Client/Delete/{id}").Result;
                Clients.Remove(clientToDelete);
            }
        }
        public IEnumerable<ClientDTO> Search(string query)
        {
            return Clients
                .Where(c => c.Name.ToUpper()
                .Contains(query.ToUpper()));
        }
    }
}
