using VirtualExpo.Bll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using Microsoft.AspNetCore.Authorization;

namespace VirtualExpo.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Admin")]
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

        public IActionResult AddEditUser(int id = 0)
        {
            BllUser blluser = new BllUser();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Organizer";
                ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit Organizer";
                ViewBag.IsAdd = false;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/Users/AddEditUser.cshtml");
        }

        public IActionResult Exhibitionindex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Exhibition/Index.cshtml");
        }
        public IActionResult AttendeeIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Attendee/Index.cshtml");
        }

        public IActionResult RequestByOrganizers()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/RequestAdmin/Index.cshtml");
        }

        public IActionResult AddEditAttendee(int id)
        {
            BllUser blluser = new BllUser();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Attendee";
                ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit Attendee";
                ViewBag.IsAdd = false;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/Attendee/AddEditAttendee.cshtml");
        }

        public IActionResult AddEditExhibition(int id = 0)
        {
            BllExhibition bllExhibition = new BllExhibition();
            if (bllExhibition.GetByPK(id) == null)
            {
                Exhibition dbExhibition = new Exhibition();
                ViewBag.data = dbExhibition;
                ViewBag.title = "Add Exhibition";
                ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = bllExhibition.GetByPK(id);
                ViewBag.title = "Edit Exhibition  ";
                ViewBag.IsAdd = false;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/Exhibition/AddEditExhibition.cshtml");
        }

    }
}
