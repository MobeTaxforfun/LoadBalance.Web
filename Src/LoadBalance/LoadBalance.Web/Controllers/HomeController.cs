using LoadBalance.Web.Models;
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
            ViewBag.WebAppHostName = _configuration["WebAppHostName"];
            ViewBag.HostName = _configuration["HOSTNAME"];
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