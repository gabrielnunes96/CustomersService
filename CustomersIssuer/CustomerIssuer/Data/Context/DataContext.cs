global using Microsoft.EntityFrameworkCore;
using CustomerIssuer.Models;

namespace CustomerIssuer.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<Transaction>? Transactions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CustomerIssuer"));
        }
    }
}