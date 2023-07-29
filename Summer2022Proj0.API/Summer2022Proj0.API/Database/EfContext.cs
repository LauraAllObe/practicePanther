using Microsoft.EntityFrameworkCore;
using Summer2022Proj0.library.Models;

namespace Summer2022Proj0.API.Database
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Time> Times { get; set; }
    }
}