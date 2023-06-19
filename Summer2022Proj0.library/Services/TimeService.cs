using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Summer2022Proj0.library.Models;

namespace Summer2022Proj0.library.Services
{
    public class TimeService
    {
        private static TimeService? instance;
        private static object _lock = new object();
        public static TimeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TimeService();
                    }
                }
                return instance;
            }
        }
        private List<Time> times;
        private TimeService()
        {
            //times = new List<Time>();
            times = new List<Time>
            {
                new Time{Id = 1, Date = DateTime.Today, Narrative = "did a lot of stuff", Hours = 2.5, ProjectId = 0, EmployeeId = 0},
                new Time{Id = 2, Date = DateTime.MaxValue, Narrative = "not yet done", Hours = 5, ProjectId = 1, EmployeeId = 2},
                new Time{Id = 3, Date = DateTime.MinValue, Narrative = "completed everything", Hours = 10, ProjectId = 15, EmployeeId = 12}
            };
        }
        public List<Time> Times
        {
            get
            {
                return times;
            }
        }
        public Time? Get(int id)//? means returns explicit null
        {
            return times.FirstOrDefault(e => e.Id == id);
        }
        public void Add(Time? time)
        {
            bool timeExists = false;
            int validId = 0;
            for (int i = 1; i > 0; i++)
            {
                foreach (Time? each in times)
                {
                    if (i == each.Id)
                    {
                        timeExists = true;
                        break;
                    }
                }
                if (timeExists == false)
                {
                    validId = i;
                    break;
                }
                timeExists = false;
            }
            if (validId != 0 && time != null)
                time.Id = validId;
            if (time != null)
            {
                times.Add(time);
            }
        }
        public void Delete(int id)
        {
            var timeToRemove = Get(id);
            if (timeToRemove != null)
            {
                times.Remove(timeToRemove);
            }
        }
        public void Delete(Time s)
        {
            Delete(s.Id);
        }
        public List<Time> Search(string query)
        {
            return times.Where(s => s.Date.ToString().ToUpper().Contains(query.ToUpper())).ToList();
        }

        public bool isValid(int EmployeeId, int ProjectId)
        {
            bool empIdValid = false;
            foreach (Employee e in EmployeeService.Current.Employees)
            {
                if(EmployeeId == e.Id)
                    empIdValid = true;
            }
            bool proIdValid = false;
            foreach (Client c in ClientService.Current.Clients)
            {
                foreach (Project p in c.Projects)
                {
                    if (ProjectId == p.Id)
                        proIdValid = true;
                }
            }
            if (proIdValid == true && empIdValid == true)
                return true;
            return false;
        }
    }
}
