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
    public class ProjectService
    {
        private static ProjectService? instance;
        private static object _lock = new object();
        public static ProjectService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ProjectService();
                    }
                }
                return instance;
            }
        }
        private ProjectService()
        {
            var response = new WebRequestHandler()
                    .Get("/Project")
                    .Result;
            projects = JsonConvert
                .DeserializeObject<List<ProjectDTO>>(response)
                ?? new List<ProjectDTO>();
        }
        private List<ProjectDTO> projects;
        public List<ProjectDTO> Projects
        {
            get
            {
                return projects ?? new List<ProjectDTO>();
            }
        }
        public ProjectDTO? Get(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }
        public void AddOrEdit(ProjectDTO p)
        {
            var response
                = new WebRequestHandler().Post("/Project", p).Result;
            //MISSING CODE
            var myEditedProject = JsonConvert.DeserializeObject<ProjectDTO>(response);
            if (myEditedProject != null)
            {
                var existingProject = projects.FirstOrDefault(p => p.Id == myEditedProject.Id);
                if (existingProject == null)
                {
                    projects.Add(myEditedProject);
                }
                else
                {
                    var index = projects.IndexOf(existingProject);
                    projects.Remove(existingProject);
                    projects.Insert(index, myEditedProject);
                }
            }
        }
        public void Delete(int id)
        {
            var projectToDelete = Projects.FirstOrDefault(p => p.Id == id);
            if (projectToDelete != null)
            {
                var response
                = new WebRequestHandler().Delete($"/Project/Delete/{id}").Result;
                Projects.Remove(projectToDelete);
            }
        }
        public IEnumerable<ProjectDTO> Search(string query)
        {
            return Projects
                .Where(p => (p.ShortName + p.LongName).ToUpper()
                .Contains(query.ToUpper()));
        }
    }
}
