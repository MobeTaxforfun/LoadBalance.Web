using LoadBalance.Web.Consul;
using LoadBalance.Web.Models.Context;
using LoadBalance.Web.Models.Migration;
using LoadBalance.Web.Models.SeedData;
using LoadBalance.Web.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

#region About config
var dbserver = configuration.GetValue<string>("dbserver");
var dbname = configuration.GetValue<string>("dbname");
var dbuser = configuration.GetValue<string>("dbuser");
var dbpw = configuration.GetValue<string>("dbpw");
var webappmsg = configuration.GetValue<string>("webappmsg");
#endregion
Console.WriteLine($"webappmsg:{webappmsg}");
Console.WriteLine($"dbconnect:Server={dbserver};Database={dbname};User={dbuser};Password={dbpw};");

// Add services to the container.
builder.Services.AddControllersWithViews();
if (string.IsNullOrEmpty(dbserver))
{
    builder.Services.AddTransient<IFreeRapidService, FreeRapidTempSevice>();
}
else
{
    builder.Services.AddTransient<IFreeRapidService, FreeRapidService>();
    builder.Services.AddDbContext<LoadBalanceDBContext>(
        option =>
        {
            option.UseSqlServer($"Server={dbserver};Database={dbname};User={dbuser};Password={dbpw};");
        });
}


var app = builder.Build();

if (!string.IsNullOrEmpty(dbserver))
{
    app.Services.MigrationTestDb();
    app.Services.EnsureSeedData();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseConsul(configuration);
app.Run();
