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
    [Authorize(Roles = "Organizer", AuthenticationSchemes = "Organizer")]
    public class OrganizerController : Controller
    {
        public IActionResult UserIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Users/ExhibitorIndex.cshtml");
        }
        public IActionResult Dashboard()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Dashboard/Index.cshtml");
        }

        public IActionResult RequestByOrganizer()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/RequestOrganizer/Index.cshtml");
        }


        public IActionResult AddEditUser(int id = 0)
        {
            BllUser blluser = new BllUser();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Exhibitor";
                ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit Exhibitor";
                ViewBag.IsAdd = false;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/Users/AddEditExhibitor.cshtml");
        }
        public IActionResult Exhibitionindex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Exhibition/Index.cshtml");
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
