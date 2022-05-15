using Consul;

namespace LoadBalance.Web.Consul
{
    public static class ConsulExtensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var test = Program.IP;
            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri("http://localhost:8500/");
                c.Datacenter = "dc1";
            });

            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "testservice3",
                Name = "test3",
                Address = "host.docker.internal", 
                Port = 7180, 
                Tags = new string[] { "App.Web" },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"https://host.docker.internal:7180/Health/Heartbeat",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                }

            }).Wait();


            lifetime.ApplicationStopping.Register(() =>
            {
                client.Agent.ServiceDeregister("testservice1").Wait();
            });

            return app;
        }
    }
}
