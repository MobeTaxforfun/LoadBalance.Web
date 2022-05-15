using LoadBalance.Web.Models;
using LoadBalance.Web.Models.Context;
using LoadBalance.Web.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoadBalance.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFreeRapidService freeRapidService;
        private readonly IConfiguration _configuration;

        public HomeController(
            ILogger<HomeController> logger, 
            IFreeRapidService freeRapidService,
            IConfiguration configuration)
        {
            _logger = logger;
            this.freeRapidService = freeRapidService;
            this._configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.Webappmsg = _configuration["webappmsg"] ?? "一片安靜，毫無反應";
            ViewBag.HostName = _configuration["HOSTNAME"] ?? "localhost";
            ViewBag.dbserver = _configuration["dbserver"] ?? "none";
            ViewBag.dbname = _configuration["dbname"] ?? "none";

            return View(this.freeRapidService.LitedFreeRapid());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}