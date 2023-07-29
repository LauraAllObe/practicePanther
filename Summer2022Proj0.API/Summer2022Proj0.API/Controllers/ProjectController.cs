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
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ProjectDTO> Get()
        {
            return new ProjectEC().Search();
        }

        [HttpGet("{id}")]
        public ProjectDTO? GetId(int id)
        {
            return new ProjectEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public ProjectDTO? Delete(int id)
        {
            return new ProjectEC().Delete(id);
        }

        [HttpPost]
        public ProjectDTO AddOrEdit([FromBody] ProjectDTO project)
        {
            return new ProjectEC().AddOrEdit(project);
        }

        [HttpPost("Search")]
        public IEnumerable<ProjectDTO> Search([FromBody] QueryMessage query)
        {
            return new ProjectEC().Search(query.Query);
        }
    }
}
