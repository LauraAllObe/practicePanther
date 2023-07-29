using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Summer2022Proj0.API.EC;
using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.Utilities;
using Summer2022Proj0.library.DTO;

namespace Summer2022Proj0.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ClientDTO> Get()
        {
            return new ClientEC().Search();
        }

        [HttpGet("{id}")]
        public ClientDTO? GetId(int id)
        {
            return new ClientEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public ClientDTO? Delete(int id)
        {
            return new ClientEC().Delete(id);
        }

        [HttpPost]
        public ClientDTO AddOrEdit([FromBody] ClientDTO client)
        {
            return new ClientEC().AddOrEdit(client);
        }

        [HttpPost("Search")]
        public IEnumerable<ClientDTO> Search([FromBody] QueryMessage query)
        {
            return new ClientEC().Search(query.Query);
        }
    }
}
