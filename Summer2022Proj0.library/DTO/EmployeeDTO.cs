using Summer2022Proj0.library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer2022Proj0.library.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO()
        {
            Name = "Default Name";
            Rate = 0;
        }
        public EmployeeDTO(Employee e)
        {
            this.Id = e.Id;
            this.Name = e.Name;
            this.Rate = e.Rate;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Rate { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name} paid {Rate.ToString("F2")} an hour";
        }
    }
}