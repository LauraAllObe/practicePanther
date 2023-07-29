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
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;

        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TimeDTO> Get()
        {
            return new TimeEC().Search();
        }

        [HttpGet("{id}")]
        public TimeDTO? GetId(int id)
        {
            return new TimeEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public TimeDTO? Delete(int id)
        {
            return new TimeEC().Delete(id);
        }

        [HttpPost]
        public TimeDTO AddOrEdit([FromBody] TimeDTO time)
        {
            return new TimeEC().AddOrEdit(time);
        }

        [HttpPost("Search")]
        public IEnumerable<TimeDTO> Search([FromBody] QueryMessage query)
        {
            return new TimeEC().Search(query.Query);
        }
    }
}
