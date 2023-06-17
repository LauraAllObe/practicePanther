using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer2022Proj0.library.Models
{
    public class Time
    {
        public DateTime Date { get; set; }
        public string Narrative { get; set; }
        public double Hours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}
