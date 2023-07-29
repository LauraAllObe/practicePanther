using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Summer2022Proj0.API.EC
{
    public class ProjectEC
    {
        private EfContext ef = new EfContextFactory().CreateDbContext(new string[0]);
        public ProjectDTO AddOrEdit(ProjectDTO dto)
        {
            var project = new Project(dto);
            if (dto.Id <= 0)
                ef.Projects.Add(project);
            else
                ef.Projects.Update(project);
            ef.SaveChanges();

            return new ProjectDTO(project);
        }

        public ProjectDTO? Get(int id)
        {
            var returnVal = ef.Projects
                    .FirstOrDefault(p => p.Id == id)
                    ?? new Project();
            return new ProjectDTO(returnVal);
        }

        public ProjectDTO? Delete(int id)
        {
            var project = ef.Projects.FirstOrDefault(p => p.Id == id);
            if (project != null)
                ef.Projects.Remove(project);
            ef.SaveChanges();
            return project != null ? new ProjectDTO(project) : null;
        }

        public IEnumerable<ProjectDTO> Search(string query = "")
        {
            IEnumerable<ProjectDTO> returnVal = ef.Projects
                    .Where(p => (p.LongName + p.ShortName).ToUpper()
                    .Contains(query.ToUpper()))
                    .Take(1000)
                    .Select(p => new ProjectDTO(p));
            return returnVal;
        }
    }
}
