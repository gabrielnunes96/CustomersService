global using Microsoft.EntityFrameworkCore;
namespace CustomerService.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Card>? Cards { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Docker"));
        }
    }
}