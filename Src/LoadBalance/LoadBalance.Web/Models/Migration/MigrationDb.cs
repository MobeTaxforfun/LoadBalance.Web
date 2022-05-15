using LoadBalance.Web.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace LoadBalance.Web.Models.Migration
{
    public static class MigrationDb
    {
        public static void MigrationTestDb(this IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<LoadBalanceDBContext>();
                if (!dbcontext.Database.CanConnect())
                {
                    dbcontext.Database.Migrate();
                    dbcontext.Database.EnsureCreated();
                }
            }
        }
    }
}
