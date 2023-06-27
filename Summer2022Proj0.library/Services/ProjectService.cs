﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Summer2022Proj0.library.Models;

namespace Summer2022Proj0.library.Services
{
    public class ProjectService
    {
        private List<Project> projects;
        public List<Project> Projects
        {
            get
            {
                return projects;
            }
        }
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
            projects = new List<Project>()
            {
                new Project{ClientId = 1, Id=1, ClosedDate=DateTime.Now, OpenDate=DateTime.Today, IsActive=true, LongName="longname1", ShortName="shortname1"},
                new Project{ClientId = 1, Id=2, ClosedDate=DateTime.Now, OpenDate=DateTime.Today, IsActive=true, LongName="longname2", ShortName="shortname2"}
            };
        }
        public Project? Get(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Project? project)
        {
            if (project != null)
            {
                if (project.Id == 0)
                {
                    project.Id = LastId + 1;
                }
                projects.Add(project);
            }
        }
        
        public void Delete(int projId)
        {
            Delete(projId);
        }
        /*
        public void Delete(int id)
        {
            var enrollmentToRemove = Get(id);
            if (enrollmentToRemove != null)
            {
                projects.Remove(enrollmentToRemove);
            }
        }
        public void Delete(Project s)
        {
            Delete(s.Id);
        }
        public List<Project> Search(string query)
        {
            return projects.Where(s => s.ShortName.ToUpper().Contains(query.ToUpper())).ToList();
        }*/

        private int LastId
        {
            get
            {
                return Projects.Any() ? Projects.Select(c => c.Id).Max() : 0;
            }
        }
    }
}