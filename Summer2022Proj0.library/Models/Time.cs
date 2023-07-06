using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Summer2022Proj0.library.Models
{
    public class Time
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Narrative { get; set; }
        public double Hours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

        public bool Billed { get; set; }
        public bool wantToBill { get; set; }
        public Time()
        {
            Id = 0;
            Date = DateTime.Now;
            Narrative = "Default Narrative";
            Hours = 0;
            EmployeeId = 0;
            ProjectId = 0;
            wantToBill = true;
            Billed = false;
        }
        public override string ToString()
        {
            string pt1 = $"{Id}. {Hours.ToString("F2")} hour time entry for project {ProjectId} belongs to employee {EmployeeId}. Time entry on {Date}. Narrative: {Narrative}";
            string pt2 = "";
            if (Billed == true)
                pt2 = ". Billed. ";
            else if (wantToBill == false)
                pt2 = ". Billing disabled. ";
            else
                pt2 = ". Not yet Billed. ";
            return pt1 + pt2;
        }
    }
}
