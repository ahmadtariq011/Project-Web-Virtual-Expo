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

        public IActionResult RequestOrganizer()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/RequestOrganizer/Index.cshtml");
        }

        public IActionResult ViewRequestOrganizer(int id = 0)
        {
            BllRequestOrganizer bllRequestOrganizer = new BllRequestOrganizer();
            if (bllRequestOrganizer.GetByPK(id) == null)
            {
                //User dbuser = new User();
                //ViewBag.data = dbuser;
                //ViewBag.title = "Organizer's Request";
                //ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = bllRequestOrganizer.GetByPK(id);
                ViewBag.title = "Exhibitor's Request";
                ViewBag.IsAdd = true;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/RequestOrganizer/RequestOrganizer.cshtml");
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
                ViewBag.IsAdd = true;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/Exhibition/AddEditExhibition.cshtml");
        }

        public IActionResult Feedback()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Feedback.cshtml");
        }
        public IActionResult ExhibitorIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Users/ExhibitorIndex.cshtml");
        }
        public IActionResult AddEditExhibitor(int id = 0)
        {
            BllUser blluser = new BllUser();

            BllExhibitorDescription bllExhibitorDescription = new BllExhibitorDescription();

            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Exhibitor";
                ViewBag.IsAdd = false;
                ExhibitorDescription dbWorkExperience = new ExhibitorDescription();
                ViewBag.ExhibitorDescription = dbWorkExperience;
            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit Exhibitor";
                ViewBag.IsAdd = false;
                var WorkingExperiencedata = bllExhibitorDescription.GetByUserid(ViewBag.data.Id);
                if (bllExhibitorDescription.GetByUserid(ViewBag.data.Id) != null)
                {
                    ViewBag.ExhibitorDescription = bllExhibitorDescription.GetByUserid(ViewBag.data.Id);
                }
                else
                {
                    ExhibitorDescription dbWorkExperience = new ExhibitorDescription();
                    ViewBag.ExhibitorDescription = dbWorkExperience;
                }
            }
            return View("Views/ExpoAdmin/ExpoDashboard/Attendee/AddEditExhibitor.cshtml");
        }
        public IActionResult AttendeeIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Attendee/Index.cshtml");
        }
    }
}
