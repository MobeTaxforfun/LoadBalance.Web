using Consul;
using System.Net;
using System.Net.Sockets;

namespace LoadBalance.Web.Consul
{
    public static class ConsulExtensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var webappname = configuration["webappmsg"] ?? "未知的服務";
            var consulhost = configuration["consulhost"] ?? "未配置服務發現";
            if(configuration["consulhost"] == null)
            {
                return app;
            }

            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var name = Dns.GetHostName();
            var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            var AppId = "LoadbalanceWeb" + Guid.NewGuid().ToString("N");

            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{consulhost}:8500/");
                c.Datacenter = "dc1";
            });

            lifetime.ApplicationStarted.Register(() => {
                client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = AppId,
                    Name = webappname,
                    Address = name,
                    Port = 80,
                    Tags = new string[] { "App.Web" },
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(10),
                        HTTP = $"http://{name}/Health/Heartbeat",
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
                    }

                }).Wait();

            });

            lifetime.ApplicationStopping.Register(() =>
            {
                client.Agent.ServiceDeregister(AppId).Wait();
            });

            return app;
        }
    }
}
