using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Summer2022Proj0.API.Database
{
    public class EfContextFactory
        : IDesignTimeDbContextFactory<EfContext>
    {
        static string? connectionString = null;
        static EfContextFactory()
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            connectionString = config["ConnectionStrings:Proj0_DB4"];
        }
        public EfContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EfContext(optionsBuilder.Options);
        }
    }
}
