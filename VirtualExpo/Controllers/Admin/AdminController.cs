using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualExpo.Web.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult UserIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Users/Index.cshtml");
        }
        public IActionResult Dashboard()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Dashboard/Index.cshtml");
        }
    }
}
