using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Models;

namespace VirtualExpo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("Views/ExpoAdmin/Login.cshtml");
        }
        public IActionResult HomeIndex()
        {
            return View("Views/ExpoHome/Home/Index.cshtml");
        }
        public IActionResult ContactUs()
        {
            return View("Views/ExpoHome/ContactUs/Index.cshtml");
        }
        public IActionResult ExhibitionLive()
        {
            return View("Views/ExpoHome/Exhibition/ExhibitionHome/ExibitionHome.cshtml");
        }
        public IActionResult AboutUs()
        {
            return View("Views/ExpoHome/AboutUs/Index.cshtml");
        }
        public IActionResult EventHome()
        {
            return View("Views/ExpoHome/Events/Home/Index.cshtml");
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
