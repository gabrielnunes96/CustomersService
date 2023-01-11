using ClientAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Client>? Clients { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>(new ClientMapping().Configure);
        }
    }
}
