using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Bll;
using VirtualExpo.Model.Data;
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
        public IActionResult Register(int id)
        {
            BllUser blluser = new BllUser();

            BllEducation bllEducation = new BllEducation();
            BllWorkingExperience bllWorkingExperience = new BllWorkingExperience();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Register";
                ViewBag.IsAdd = false;
                WorkExperience dbWorkExperience = new WorkExperience();
                ViewBag.WorkingExperiencedata = dbWorkExperience;
                Education dbEducation = new Education();

                ViewBag.Educationdata = dbEducation;
            }
            else
            {

                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "My Profile";
                ViewBag.IsAdd = false;
                ViewBag.WorkingExperiencedata = bllWorkingExperience.GetByAttendeeId(ViewBag.data.Id);

                ViewBag.Educationdata = bllEducation.GetByAttendeeId(ViewBag.data.Id);
            }
            return View("Views/ExpoHome/Account/Register.cshtml");
        }
       
        public IActionResult Login()
        {
            return View("Views/Login.cshtml");
        }
        public IActionResult LoginAtenee()
        {
            return View("Views/ExpoHome/Login.cshtml");
        }
        public IActionResult HomeIndex()
        {
            return View("Views/ExpoHome/Home/Index.cshtml");
        }
        public IActionResult ContactUs()
        {
            return View("Views/ExpoHome/ContactUs/Index.cshtml");
        }
        public IActionResult AboutUs()
        {
            return View("Views/ExpoHome/AboutUs/Index.cshtml");
        }
        public IActionResult EventHome()
        {
            return View("Views/ExpoHome/Events/Home/Index.cshtml");
        }
        public IActionResult RequestOrganizer()
        {
            ViewBag.title = "Book your Stall";
            return View("Views/ExpoHome/Requests/RequestOrganizer.cshtml");
        }
        public IActionResult RequestAdmin()
        {
            ViewBag.title = "Book Event";
            return View("Views/ExpoHome/Requests/RequestAdmin.cshtml");
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
