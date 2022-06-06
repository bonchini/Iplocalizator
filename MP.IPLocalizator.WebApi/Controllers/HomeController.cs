using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MP.IPLocalizator.IServices;
using MP.IPLocalizator.WebApi.Models;

namespace MP.IPLocalizator.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocalizatorService localizatorService;
        public HomeController(ILocalizatorService localizatorService)
        {
            this.localizatorService = localizatorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Metrics()
        {
            var distances = this.localizatorService.GetMetrics();
            ViewBag.Distances = distances;
            return View();
        }


        public async Task<IActionResult> LocalizeIp(string ip)
        {
            var result = await this.localizatorService.Localize(ip);
            ViewBag.IpData = result;
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
