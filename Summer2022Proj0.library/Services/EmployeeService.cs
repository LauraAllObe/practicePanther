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
    public class EmployeeService
    {
        private static EmployeeService? instance;
        private static object _lock = new object();
        public static EmployeeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeService();
                    }
                }
                return instance;
            }
        }
        private EmployeeService()
        {
            var response = new WebRequestHandler()
                    .Get("/Employee")
                    .Result;
            employees = JsonConvert
                .DeserializeObject<List<EmployeeDTO>>(response) 
                ?? new List<EmployeeDTO>();
            /*
            employees = new List<Employee>
            {
                new Employee{Id = 1, Name = "Henry Avery", Rate = Decimal.Parse("7.50")},
                new Employee{Id = 2, Name = "Hiram Beaks", Rate = Decimal.Parse("12.25")},
                new Employee{Id = 3, Name = "Jack Sparrow", Rate = 18}
            };*/

        }
        private List<EmployeeDTO> employees;
        public List<EmployeeDTO> Employees
        {
            get
            {
                return employees ?? new List<EmployeeDTO>();
            }
        }
        public EmployeeDTO? Get(int id)
        {
            /*var response = new WebRequestHandler()
                .Get("/Client/GetClients/{id}")
                .Result;
            var employee = JsonConvert.DeserializeObject<Employee>(response);
            return employee;*/
            return Employees.FirstOrDefault(e =>  e.Id == id);
        }
        public void AddOrEdit(EmployeeDTO e)
        {
            var response 
                = new WebRequestHandler().Post("/Employee", e).Result;
            //MISSING CODE
            var myEditedEmployee = JsonConvert.DeserializeObject<EmployeeDTO>(response);
            if(myEditedEmployee != null)
            {
                var existingEmployee = employees.FirstOrDefault(e => e.Id == myEditedEmployee.Id);
                if (existingEmployee == null)
                {
                    employees.Add(myEditedEmployee);
                }
                else
                {
                    var index = employees.IndexOf(existingEmployee);
                    employees.Remove(existingEmployee);
                    employees.Insert(index, myEditedEmployee);
                }
            }
        }
        public void Delete(int id)
        {
            /*
            var employeeToRemove = Get(id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }*/

            /*
             var response = new WebRequestHandler()
                .Get("/Client/GetClients/{id}")
                .Result;
            var employee = JsonConvert.DeserializeObject<Employee>(response);
            return employee;
             */
            //MISSING CODE
            var employeeToDelete = Employees.FirstOrDefault(e => e.Id == id);
            if(employeeToDelete != null)
            {
                var response
                = new WebRequestHandler().Delete($"/Employee/Delete/{id}").Result;
                Employees.Remove(employeeToDelete);
            }
        }
        public IEnumerable<EmployeeDTO> Search(string query)
        {
            return Employees
                .Where(s => s.Name.ToUpper()
                .Contains(query.ToUpper()));
        }
    }
}
