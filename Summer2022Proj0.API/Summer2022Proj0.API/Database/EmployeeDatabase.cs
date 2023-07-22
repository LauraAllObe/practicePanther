using Summer2022Proj0.library.Models;
using Summer2022Proj0.API.EC;

namespace Summer2022Proj0.API.Database
{
    public static class EmployeeDatabase
    {
        public static List<Employee> Employees = new List<Employee>
        {
            new Employee{Id = 1, Name = "Henry Avery", Rate = Decimal.Parse("7.50")},
            new Employee{Id = 2, Name = "Hiram Beaks", Rate = Decimal.Parse("12.25")},
            new Employee{Id = 3, Name = "Jack Sparrow", Rate = 18}
        };

        public static int LastEmployeeId
            => Employees.Any() ? Employees.Select(e => e.Id).Max() : 0;
    }

}