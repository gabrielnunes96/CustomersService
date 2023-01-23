global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClientAPI.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Client>? Clients { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString("DemoMicroserviceOne"));
        }
    }
}
