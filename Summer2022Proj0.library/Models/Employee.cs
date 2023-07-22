using Summer2022Proj0.library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Summer2022Proj0.library.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public Employee()
        {
            Name = "Default Name";
            Id = 0;
            Rate = 0;
        }
        public Employee(EmployeeDTO dto)
        {
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.Rate = dto.Rate;
        }
        public override string ToString()
        {
            return $"{Id}. {Name} paid {Rate.ToString("F2")} an hour";
        }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
        public string Property6 { get; set; }
        public string Property7 { get; set; }
        public string Property8 { get; set; }
        public string Property9 { get; set; }
        public string Property10 { get; set; }
    }
}
