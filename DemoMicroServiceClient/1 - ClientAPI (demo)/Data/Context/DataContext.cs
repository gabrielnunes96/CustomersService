global using Microsoft.EntityFrameworkCore;

namespace ClientAPI.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Client>? Clients { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-IT6RHKB\\SQLEXPRESS;Database=DemoMicroserviceOne;Trusted_Connection=True;TrustServerCertificate=true");
        }
    }
}
