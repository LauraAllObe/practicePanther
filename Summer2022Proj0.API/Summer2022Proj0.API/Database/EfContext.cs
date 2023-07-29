using Microsoft.EntityFrameworkCore;
using Summer2022Proj0.library.Models;

namespace Summer2022Proj0.API.Database
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}