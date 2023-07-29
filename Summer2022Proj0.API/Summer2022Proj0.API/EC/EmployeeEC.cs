using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Summer2022Proj0.API.EC
{
    public class EmployeeEC
    {
        private EfContext ef = new EfContextFactory().CreateDbContext(new string[0]);
        public EmployeeDTO AddOrEdit(EmployeeDTO dto)
        {
            var employee = new Employee(dto);
            if(dto.Id <= 0)
                ef.Employees.Add(employee);
            else
                ef.Employees.Update(employee);
            ef.SaveChanges();
            
            return new EmployeeDTO(employee);
        }

        public EmployeeDTO? Get(int id)
        {
            var returnVal = ef.Employees
                    .FirstOrDefault(e => e.Id == id)
                    ?? new Employee();
            return new EmployeeDTO(returnVal);
        }

        public EmployeeDTO? Delete(int id)
        {
            var employee = ef.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
                ef.Employees.Remove(employee);
            ef.SaveChanges();
            return employee != null ? new EmployeeDTO(employee) : null;
        }

        public IEnumerable<EmployeeDTO> Search(string query = "")
        {
            IEnumerable<EmployeeDTO> returnVal = ef.Employees
                    .Where(e => e.Name.ToUpper()
                    .Contains(query.ToUpper()))
                    .Take(1000)
                    .Select(e => new EmployeeDTO(e));
            return returnVal;
        }
    }
}
