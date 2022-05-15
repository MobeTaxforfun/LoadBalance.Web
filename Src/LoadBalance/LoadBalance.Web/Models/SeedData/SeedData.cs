using LoadBalance.Web.Models.Context;

namespace LoadBalance.Web.Models.SeedData
{
    public static class SeedData
    {
        public static void EnsureSeedData(this IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<LoadBalanceDBContext>();
                if (!dbcontext.FreeRapid.Any())
                {
                    dbcontext.FreeRapid.Add(new Entity.FreeRapid
                    {
                        Id = 1,
                        City = "臺北市",
                        Town = "士林區",
                        RapidName = "粱耳鼻喉科診所",
                        Address = "臺北市士林區社正路12-1號",
                        PhoneNumber = "02-28168456",
                        Lat = 121.509303,
                        Long = 25.08855
                    });
                    dbcontext.SaveChanges();
                }
            }
        }
    }
}
