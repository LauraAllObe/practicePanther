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
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<EmployeeDTO> Get()
        {
            return new EmployeeEC().Search();
        }

        [HttpGet("{id}")]
        public EmployeeDTO? GetId(int id)
        {
            return new EmployeeEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public EmployeeDTO? Delete(int id)
        {
            return new EmployeeEC().Delete(id);
        }

        [HttpPost]
        public EmployeeDTO AddOrEdit([FromBody]EmployeeDTO employee)
        {
            return new EmployeeEC().AddOrEdit(employee);
        }

        [HttpPost("Search")]
        public IEnumerable<EmployeeDTO> Search([FromBody]QueryMessage query)
        {
            return new EmployeeEC().Search(query.Query);
        }
    }
}
