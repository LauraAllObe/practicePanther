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
        private List<Employee> employees;
        private EmployeeService()
        {
            employees = new List<Employee>();
            /*
            employees = new List<Employee>
            {
                new Employee{Id = 1, Name = "Henry Avery", Rate = 15},
                new Employee{Id = 2, Name = "Jack Sparrow", Rate = 18},
                new Employee{Id = 3, Name = "Tom Junior", Rate = 12}
            };*/

        }
        public List<Employee> Employees
        {
            get
            {
                return employees;
            }
        }
        public Employee? Get(int id)//? means returns explicit null
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }
        public void Add(Employee? employee)
        {
            bool employeeExists = false;
            int validId = 0;
            for (int i = 1; i > 0; i++)
            {
                foreach (Employee? each in employees)
                {
                    if (i == each.Id)
                    {
                        employeeExists = true;
                        break;
                    }
                }
                if (employeeExists == false)
                {
                    validId = i;
                    break;
                }
                employeeExists = false;
            }
            if (validId != 0 && employee != null)
                employee.Id = validId;
            if (employee != null)
            {
                employees.Add(employee);
            }
        }
        public void Delete(int id)
        {
            var employeeToRemove = Get(id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }
        }
        public void Delete(Employee s)
        {
            Delete(s.Id);
        }
        public List<Employee> Search(string query)
        {
            return employees.Where(s => s.Name.ToUpper().Contains(query.ToUpper())).ToList();
        }
    }
}
