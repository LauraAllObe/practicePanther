using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Summer2022Proj0.API.EC
{
    public class ClientEC
    {
        private EfContext ef = new EfContextFactory().CreateDbContext(new string[0]);
        public ClientDTO AddOrEdit(ClientDTO dto)
        {
            var client = new Client(dto);
            if (dto.Id <= 0)
                ef.Clients.Add(client);
            else
                ef.Clients.Update(client);
            ef.SaveChanges();

            return new ClientDTO(client);
        }

        public ClientDTO? Get(int id)
        {
            var returnVal = ef.Clients
                    .FirstOrDefault(c => c.Id == id)
                    ?? new Client();
            return new ClientDTO(returnVal);
        }

        public ClientDTO? Delete(int id)
        {
            var client = ef.Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                ef.Clients.Remove(client);
            ef.SaveChanges();
            return client != null ? new ClientDTO(client) : null;
        }

        public IEnumerable<ClientDTO> Search(string query = "")
        {
            IEnumerable<ClientDTO> returnVal = ef.Clients
                    .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()))
                    .Take(1000)
                    .Select(c => new ClientDTO(c));
            return returnVal;
        }
    }
}
