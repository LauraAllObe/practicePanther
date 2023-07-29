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
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> _logger;

        public BillController(ILogger<BillController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<BillDTO> Get()
        {
            return new BillEC().Search();
        }

        [HttpGet("{id}")]
        public BillDTO? GetId(int id)
        {
            return new BillEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public BillDTO? Delete(int id)
        {
            return new BillEC().Delete(id);
        }

        [HttpPost]
        public BillDTO AddOrEdit([FromBody] BillDTO bill)
        {
            return new BillEC().AddOrEdit(bill);
        }

        [HttpPost("Search")]
        public IEnumerable<BillDTO> Search([FromBody] QueryMessage query)
        {
            return new BillEC().Search(query.Query);
        }
    }
}
