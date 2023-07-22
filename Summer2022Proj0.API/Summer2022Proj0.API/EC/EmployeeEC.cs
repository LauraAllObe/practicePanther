using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.API.Database;

namespace Summer2022Proj0.API.EC
{
    public class EmployeeEC
    {
        public EmployeeDTO AddOrEdit(EmployeeDTO dto)
        {
            if(dto.Id > 0)
            {
                var employeeToUpdate
                    = EmployeeDatabase.Employees
                    .FirstOrDefault(e => e.Id == dto.Id);
                if(employeeToUpdate != null)
                {
                    EmployeeDatabase.Employees.Remove(employeeToUpdate);
                }
                EmployeeDatabase.Employees.Add(new Employee(dto));
            }
            else
            {
                dto.Id = EmployeeDatabase.LastEmployeeId + 1;
                EmployeeDatabase.Employees.Add(new Employee(dto));
            }
            return dto;
        }

        public EmployeeDTO? Get(int id)
        {
            var returnVal = EmployeeDatabase.Employees
                .FirstOrDefault(e => e.Id == id)
                ?? new Employee();
            return new EmployeeDTO(returnVal);
        }

        public EmployeeDTO? Delete(int id)
        {
            var employeeToDelete = EmployeeDatabase.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete != null)
                EmployeeDatabase.Employees.Remove(employeeToDelete);
            return employeeToDelete != null ?
                new EmployeeDTO(employeeToDelete)
                : null;
        }

        public IEnumerable<EmployeeDTO> Search(string query = "")
        {
            return EmployeeDatabase.Employees
                .Where(e => e.Name.ToUpper()
                .Contains(query.ToUpper()))
                .Take(1000)
                .Select(e => new EmployeeDTO(e));
        }
    }
}
