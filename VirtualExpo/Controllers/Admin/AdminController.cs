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
        public IActionResult ExhibitorIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Users/ExhibitorIndex.cshtml");
        }
        public IActionResult Dashboard()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Dashboard/Index.cshtml");
        }

        public IActionResult AddEditAdmin(int id = 0)
        {
            BllUser blluser = new BllUser();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Admin";
                ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit Admin";
                ViewBag.IsAdd = false;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/Users/AddEditAdmin.cshtml");
        }
        public IActionResult ViewRequestAdmin(int id = 0)
        {
            BllRequestAdmin bllRequestAdmin = new BllRequestAdmin();
            if (bllRequestAdmin.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Organizer's Request";
                ViewBag.IsAdd = false;

            }
            else
            {
                ViewBag.data = bllRequestAdmin.GetByPK(id);
                ViewBag.title = "Organizer's Request";
                ViewBag.IsAdd = true;

            }
            return View("Views/ExpoAdmin/ExpoDashboard/RequestAdmin/RequestAdmin.cshtml");
        }
        public IActionResult AddEditAttednee(int id = 0)
        {
            BllUser blluser = new BllUser();

            BllEducation bllEducation = new BllEducation();
            BllWorkingExperience bllWorkingExperience = new BllWorkingExperience();
            if (blluser.GetByPK(id) == null)
            {
                User dbuser = new User();
                ViewBag.data = dbuser;
                ViewBag.title = "Add Attendee";
                ViewBag.IsAdd = false;
                WorkExperience dbWorkExperience = new WorkExperience();
                ViewBag.WorkingExperiencedata = dbWorkExperience;
                Education dbEducation = new Education();

                ViewBag.Educationdata = dbEducation;
            }
            else
            {
                ViewBag.data = blluser.GetByPK(id);
                ViewBag.title = "Edit Attendee";
                ViewBag.IsAdd = false;
                ViewBag.WorkingExperiencedata = bllWorkingExperience.GetByAttendeeId(ViewBag.data.Id);

                ViewBag.Educationdata = bllEducation.GetByAttendeeId(ViewBag.data.Id);
            }
            return View("Views/ExpoAdmin/ExpoDashboard/Attendee/AddEditAttende.cshtml");
        }

        public IActionResult Exhibitionindex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Exhibition/Index.cshtml");
        }

        public IActionResult AttendeeIndex()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/Attendee/Index.cshtml");
        }

        public IActionResult RequestAdmin()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/RequestAdmin/Index.cshtml");
        }
        public IActionResult RequestOrganizer()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/RequestExhibitor/Index.cshtml");
        }

        public IActionResult ContactUs()
        {
            return View("Views/ExpoAdmin/ExpoDashboard/ContactUs.cshtml");
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
                ViewBag.IsAdd = true;
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
            return View("Views/ExpoAdmin/ExpoDashboard/Users/AddEditExhibitor.cshtml");
        }
    }
}
