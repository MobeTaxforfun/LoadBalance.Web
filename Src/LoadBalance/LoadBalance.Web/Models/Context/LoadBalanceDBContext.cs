using LoadBalance.Web.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace LoadBalance.Web.Models.Context
{
    public class LoadBalanceDBContext : DbContext
    {
        public LoadBalanceDBContext(DbContextOptions<LoadBalanceDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FreeRapid>().Property(c=>c.Id).ValueGeneratedNever();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<FreeRapid> FreeRapid { get; set; }
    }
}
