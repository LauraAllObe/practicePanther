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
        private TimeService()
        {
            var response = new WebRequestHandler()
                    .Get("/Time")
                    .Result;
            times = JsonConvert
                .DeserializeObject<List<TimeDTO>>(response)
                ?? new List<TimeDTO>();
        }
        private List<TimeDTO> times;
        public List<TimeDTO> Times
        {
            get
            {
                return times ?? new List<TimeDTO>();
            }
        }
        public TimeDTO? Get(int id)
        {
            return Times.FirstOrDefault(t => t.Id == id);
        }
        public void AddOrEdit(TimeDTO t)
        {
            var response
                = new WebRequestHandler().Post("/Time", t).Result;
            //MISSING CODE
            var myEditedTime = JsonConvert.DeserializeObject<TimeDTO>(response);
            if (myEditedTime != null)
            {
                var existingTime = times.FirstOrDefault(t => t.Id == myEditedTime.Id);
                if (existingTime == null)
                {
                    times.Add(myEditedTime);
                }
                else
                {
                    var index = times.IndexOf(existingTime);
                    times.Remove(existingTime);
                    times.Insert(index, myEditedTime);
                }
            }
        }
        public void Delete(int id)
        {
            var timeToDelete = Times.FirstOrDefault(t => t.Id == id);
            if (timeToDelete != null)
            {
                var response
                = new WebRequestHandler().Delete($"/Time/Delete/{id}").Result;
                Times.Remove(timeToDelete);
            }
        }
        public IEnumerable<TimeDTO> Search(string query)
        {
            return Times
                .Where(t => t.Narrative.ToUpper()
                .Contains(query.ToUpper()));
        }
    }
}
